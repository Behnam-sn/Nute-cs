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

        var playlistAllLines = File.ReadAllLines(path);
        Path = path;
        Title = ExtractPlaylistTitle(playlistAllLines);
        Songs = ExtractPlayListSongs(playlistAllLines);
    }

    public RemoveDuplicateSongsInPlaylistResultDto RemoveDuplicateSongsInPlaylist()
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

    public FindNonExistentSongsInPlaylistResultDto FindNonExistentSongsInPlaylist()
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

    public ComparePlaylistsResultDto CompareTo(Playlist playlist)
    {
        var inCommonSongs = new List<string>();

        foreach (var song in Songs)
        {
            if (song is "\n")
            {
                continue;
            }

            if (playlist.Songs.Contains(song))
            {
                inCommonSongs.Add(song);
            }
        }

        var playlist1Songs = Songs.ToList();
        playlist1Songs.RemoveAll(i => inCommonSongs.Contains(i));

        var playlist2Songs = playlist.Songs.ToList();
        playlist2Songs.RemoveAll(i => inCommonSongs.Contains(i));

        return new ComparePlaylistsResultDto(
            Playlist1Songs: playlist1Songs,
            Playlist2Songs: playlist2Songs,
            InCommonSongs: inCommonSongs);
    }

    public void SavePlaylist()
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
