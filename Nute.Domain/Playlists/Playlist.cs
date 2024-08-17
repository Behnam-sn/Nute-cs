using System.Text;
using Nute.Domain.Playlists.Dtos;
using Nute.Domain.Playlists.Exceptions;
using Nute.Domain.Songs;
using Nute.Domain.Songs.Exceptions;

namespace Nute.Domain.Playlists;

public sealed class Playlist
{
    private const string PLAYLIST_TYPE = ".m3u8";

    public string Path { get; }
    public string Title { get; }
    public IEnumerable<PlaylistSong> Songs { get; private set; }

    private Playlist(string path, string title, IEnumerable<PlaylistSong> songs)
    {
        Path = path;
        Title = title;
        Songs = songs;
    }

    public IEnumerable<PlaylistSong> GetNotFoundedSongs()
    {
        var notFoundedSongs = Songs.Where(i => i.Value is null);
        return notFoundedSongs;
    }

    public IEnumerable<PlaylistSong> GetDuplicateSongs()
    {
        var uniqueSongs = new HashSet<string>();
        var duplicateSongs = new List<PlaylistSong>();

        foreach (var song in Songs)
        {
            if (uniqueSongs.Add(song.Path) is false)
            {
                duplicateSongs.Add(song);
            }
        }

        return duplicateSongs;
    }

    public void RemoveDuplicateSongs()
    {
        var duplicateSongs = GetDuplicateSongs();
        Songs = Songs.Where(i => !duplicateSongs.Contains(i));
    }

    public ComparePlaylistsResultDto CompareTo(Playlist otherPlaylist)
    {
        var inCommonSongs = Songs.Where(i => otherPlaylist.Songs.Contains(i));
        var playlist1UniqueSongs = Songs.Where(i => !inCommonSongs.Contains(i));
        var playlist2UniqueSongs = otherPlaylist.Songs.Where(i => !inCommonSongs.Contains(i));

        return new ComparePlaylistsResultDto(
            Playlist1UniqueSongs: playlist1UniqueSongs,
            Playlist2UniqueSongs: playlist2UniqueSongs,
            InCommonSongs: inCommonSongs
        );
    }

    public void Sort()
    {
        Songs = Songs.OrderBy(i => i.Value);
        var index = 0;
        foreach (var song in Songs)
        {
            song.UpdateIndex(newIndex: index);
            index++;
        }
    }

    public void UpdateSongsBasePath(string oldBasePath, string newBasePath, bool isNewBasePathLinuxBased)
    {
        foreach (var song in Songs)
        {
            var newPath = song.Path.Replace(oldBasePath, newBasePath);
            if (isNewBasePathLinuxBased)
            {
                newPath = newPath.Replace("\\", "/");
            }
            song.UpdatePath(newPath: newPath);
        }
    }

    public void Save(string? destinationDirectoryPath = null)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine("#EXTM3U");
        stringBuilder.AppendLine($"#{Title}{PLAYLIST_TYPE}");

        var sortedSongs = Songs.OrderBy(i => i.Index);
        foreach (var song in sortedSongs)
        {
            stringBuilder.AppendLine(song.Path);
        }

        var path = destinationDirectoryPath is null ? Path : System.IO.Path.Combine(destinationDirectoryPath, $"{Title}{PLAYLIST_TYPE}");
        File.WriteAllText(path, stringBuilder.ToString());
    }

    public static Playlist Parse(string playlistPath)
    {
        ValidatePlaylistPath(playlistPath);

        var playlistLines = File.ReadAllLines(playlistPath);
        var title = ExtractPlaylistTitle(playlistLines);

        var songsPaths = playlistLines[2..];
        var songs = new List<PlaylistSong>();

        for (var i = 0; i < songsPaths.Length; i++)
        {
            var songPath = songsPaths[i];
            Song? song = null;
            try
            {
                song = Song.Parse(path: songPath);
            }
            catch (SongFileNotExistDomainException)
            {
            }
            catch (SongFilePathIsInvalidDomainException)
            {
            }
            finally
            {
                var playlistSong = new PlaylistSong(
                    path: songPath,
                    index: i,
                    song: song
                );
                songs.Add(playlistSong);
            }
        }

        return new Playlist(
            path: playlistPath,
            title: title,
            songs: songs
        );
    }

    private static void ValidatePlaylistPath(string playlistPath)
    {
        var fileExtension = System.IO.Path.GetExtension(playlistPath);
        if (fileExtension is not PLAYLIST_TYPE)
        {
            throw new PlaylistTypeNotAcceptableDomainException($"{fileExtension} is Not a Acceptable Playlist Type.");
        }
    }

    private static string ExtractPlaylistTitle(string[] playlistLines)
    {
        var titleLine = playlistLines[1];
        var lastIndexOfTag = titleLine.LastIndexOf(PLAYLIST_TYPE);
        // TODO: Throw exception here
        return titleLine[1..lastIndexOfTag];
    }
}
