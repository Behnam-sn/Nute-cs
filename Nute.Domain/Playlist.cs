using System.Text;
using Nute.Domain.Dtos;

namespace Nute.Domain;

public class Playlist
{
    public string Path { get; }
    public string Title { get; }
    public IEnumerable<string> Songs { get; private set; }

    public Playlist(string path)
    {
        ValidatePlaylist(path);

        Path = path;
        var allLinesOfPlaylist = File.ReadAllLines(path);
        Title = ExtractPlaylistTitle(allLinesOfPlaylist);
        Songs = ExtractPlayListSongs(allLinesOfPlaylist);
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

    public FindNonExistentSongsInPlaylistResultDto FindNonExistentSongs()
    {
        var nonExistentSongs = new List<string>();

        foreach (var song in Songs)
        {
            if (!File.Exists(song))
            {
                nonExistentSongs.Add(song);
            }
        }

        return new FindNonExistentSongsInPlaylistResultDto(
            NonExistentSongs: nonExistentSongs);
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
        stringBuilder.Append($"#{Title}.m8u3");
        stringBuilder.Append(Songs);
        File.WriteAllText(Path, stringBuilder.ToString());
    }

    private static void ValidatePlaylist(string playlistPath)
    {
        if (System.IO.Path.GetExtension(playlistPath) is not "m3u8")
        {
            throw new Exception("Not a Acceptable Playlist Type");
        }
    }

    private static string ExtractPlaylistTitle(string[] playlistAllLines)
    {
        var titleLine = playlistAllLines[1];
        var lastIndexOfTag = titleLine.LastIndexOf(".m8u3");
        return titleLine[1..lastIndexOfTag];
    }

    private static string[] ExtractPlayListSongs(string[] playlistAllLines)
    {
        return playlistAllLines[2..];
    }
}
