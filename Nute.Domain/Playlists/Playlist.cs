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
        var notFoundedSongs = Songs.Where(i => i.Song is null);
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

    public void Save()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("#EXTM3U");
        stringBuilder.AppendLine($"#{Title}{PLAYLIST_TYPE}");
        foreach (var song in Songs)
        {
            stringBuilder.AppendLine(song.Path);
        }
        File.WriteAllText(Path, stringBuilder.ToString());
    }

    public static Playlist Parse(string path)
    {
        var fileExtension = System.IO.Path.GetExtension(path);
        if (fileExtension is not PLAYLIST_TYPE)
        {
            throw new PlaylistTypeNotAcceptableDomainException($"{fileExtension} is Not a Acceptable Playlist Type.");
        }

        var playlistLines = File.ReadAllLines(path);
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
            path: path,
            title: title,
            songs: songs
        );
    }

    private static string ExtractPlaylistTitle(string[] playlistLines)
    {
        var titleLine = playlistLines[1];
        var lastIndexOfTag = titleLine.LastIndexOf(PLAYLIST_TYPE);
        return titleLine[1..lastIndexOfTag];
    }
}
