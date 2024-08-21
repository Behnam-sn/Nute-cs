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
    public IEnumerable<PlaylistItem> Items { get; private set; }

    private Playlist(string path, string title, IEnumerable<PlaylistItem> items)
    {
        Path = path;
        Title = title;
        Items = items;
    }

    public IEnumerable<PlaylistItem> GetNotFoundedSongs()
    {
        var notFoundedSongs = Items.Where(i => i.Value is null);
        return notFoundedSongs;
    }

    public IEnumerable<PlaylistItem> GetDuplicateSongs()
    {
        var uniqueSongs = new HashSet<string>();
        var duplicateSongs = new List<PlaylistItem>();

        foreach (var item in Items)
        {
            if (!uniqueSongs.Add(item.Path))
            {
                duplicateSongs.Add(item);
            }
        }

        return duplicateSongs;
    }

    public void RemoveDuplicateSongs()
    {
        var duplicateSongs = GetDuplicateSongs();
        Items = Items.Where(i => !duplicateSongs.Contains(i));
    }

    public ComparePlaylistsResultDto CompareTo(Playlist otherPlaylist)
    {
        var inCommonSongs = Items.Where(i => otherPlaylist.Items.Contains(i));
        var playlist1UniqueSongs = Items.Where(i => !inCommonSongs.Contains(i));
        var playlist2UniqueSongs = otherPlaylist.Items.Where(i => !inCommonSongs.Contains(i));

        return new ComparePlaylistsResultDto(
            Playlist1UniqueSongs: playlist1UniqueSongs,
            Playlist2UniqueSongs: playlist2UniqueSongs,
            InCommonSongs: inCommonSongs
        );
    }

    public void Sort()
    {
        Items = Items.OrderBy(i => i.Value);
        var index = 0;
        foreach (var item in Items)
        {
            item.UpdateIndex(newIndex: index);
            index++;
        }
    }

    public void UpdateSongsBasePath(string oldBasePath, string newBasePath, bool isNewBasePathLinuxBased)
    {
        foreach (var song in Items)
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

        var sortedSongs = Items.OrderBy(i => i.Index);
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
        var songs = new List<PlaylistItem>();

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
                var playlistSong = new PlaylistItem(
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
            items: songs
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
