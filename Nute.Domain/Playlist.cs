using System.Text;
using Nute.Domain.Dtos;

namespace Nute.Domain;

public sealed class Playlist
{
    private const string PLAYLIST_TYPE = ".m3u8";

    // private List<string> _notFoundedSongs = [];

    public string Path { get; }
    public string Title { get; }
    public IEnumerable<string> Songs { get; private set; }
    public IEnumerable<string> NotFoundedSongs { get; }

    private Playlist(string path, string title, IEnumerable<string> songs, IEnumerable<string> notFoundedSongs)
    {
        Path = path;
        Title = title;
        Songs = songs;
        NotFoundedSongs = notFoundedSongs;
    }

    public ComparePlaylistsResultDto CompareTo(Playlist otherPlaylist)
    {
        var inCommonSongs = new List<string>();

        foreach (var song in Songs)
        {
            if (otherPlaylist.Songs.Contains(song))
            {
                inCommonSongs.Add(song);
            }
        }

        var playlist1UniqueSongs = Songs.ToList();
        playlist1UniqueSongs.RemoveAll(i => inCommonSongs.Contains(i));

        var playlist2UniqueSongs = otherPlaylist.Songs.ToList();
        playlist2UniqueSongs.RemoveAll(i => inCommonSongs.Contains(i));

        return new ComparePlaylistsResultDto(
            Playlist1UniqueSongs: playlist1UniqueSongs,
            Playlist2UniqueSongs: playlist2UniqueSongs,
            InCommonSongs: inCommonSongs);
    }

    public void Save()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append("");
        stringBuilder.Append($"#{Title}{PLAYLIST_TYPE}");
        stringBuilder.Append(Songs);
        File.WriteAllText(Path, stringBuilder.ToString());
    }

    public static Playlist Parse(string path)
    {
        ValidatePlaylist(path);

        var playlistLines = File.ReadAllLines(path);
        var title = ExtractPlaylistTitle(playlistLines);
        var songs = ExtractPlayListSongs(playlistLines);
        var notFoundedSongs = ExtractNotFoundedSongs(playlistLines);

        return new Playlist(
            path: path,
            title: title,
            songs: songs,
            notFoundedSongs: notFoundedSongs
        );
    }

    private static void ValidatePlaylist(string playlistPath)
    {
        var fileExtension = System.IO.Path.GetExtension(playlistPath);
        if (fileExtension is not PLAYLIST_TYPE)
        {
            throw new Exception("Not a Acceptable Playlist Type");
        }
    }

    private static string ExtractPlaylistTitle(string[] playlistLines)
    {
        var titleLine = playlistLines[1];
        var lastIndexOfTag = titleLine.LastIndexOf(PLAYLIST_TYPE);
        return titleLine[1..lastIndexOfTag];
    }

    private static string[] ExtractPlayListSongs(string[] playlistLines)
    {
        return playlistLines[2..];
    }

    private static List<string> ExtractNotFoundedSongs(string[] playlistLines)
    {
        var songsPath = playlistLines[2..];
        var notFoundedSongs = new List<string>();

        foreach (var songPath in songsPath)
        {
            if (File.Exists(songPath) is false)
            {
                notFoundedSongs.Add(songPath);
            }
        }

        return notFoundedSongs;
    }

    public IEnumerable<string> GetDuplicateSongs()
    {
        var uniqueSongs = new HashSet<string>();
        var duplicateSongs = new List<string>();

        foreach (var song in Songs)
        {
            if (uniqueSongs.Add(song) is false)
            {
                duplicateSongs.Add(song);
            }
        }

        return duplicateSongs;
    }

    public RemoveDuplicateSongsInPlaylistResultDto RemoveDuplicateSongs()
    {
        var uniqueSongs = new HashSet<string>();
        var duplicateSongs = new List<string>();

        foreach (var song in Songs)
        {
            if (uniqueSongs.Add(song) is false)
            {
                duplicateSongs.Add(song);
            }
        }

        Songs = uniqueSongs;

        return new RemoveDuplicateSongsInPlaylistResultDto(
            UniqueSongs: uniqueSongs,
            DuplicateSongs: duplicateSongs);
    }
}
