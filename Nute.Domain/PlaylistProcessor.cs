using Nute.Domain.Dtos;

namespace Nute.Domain;

public class PlaylistProcessor
{
    public static ComparePlaylistsResultDto ComparePlaylists(IEnumerable<string> playlist1, IEnumerable<string> playlist2)
    {
        var inCommonSongs = new List<string>();

        foreach (var song in playlist1)
        {
            if (song is "\n")
            {
                continue;
            }

            if (playlist2.Contains(song))
            {
                inCommonSongs.Add(song);
            }
        }

        var playlist1Songs = playlist1.ToList();
        playlist1Songs.RemoveAll(i => inCommonSongs.Contains(i));

        var playlist2Songs = playlist2.ToList();
        playlist2Songs.RemoveAll(i => inCommonSongs.Contains(i));

        return new ComparePlaylistsResultDto(playlist1Songs, playlist2Songs, inCommonSongs);
    }

    public static FindDuplicateSongsInPlaylistResultDto FindDuplicateSongsInPlaylist(IEnumerable<string> playlist)
    {
        var songs = new HashSet<string>();
        var duplicateSongs = new List<string>();

        foreach (var song in playlist)
        {
            if (songs.Add(song) is false)
            {
                duplicateSongs.Add(song);
            }
        }

        return new FindDuplicateSongsInPlaylistResultDto(songs, duplicateSongs);
    }

    public static FindNonExistentSongsInPlaylistResultDto FindNonExistentSongsInPlaylist(IEnumerable<string> playlist)
    {
        var nonExistentSongs = new List<string>();

        foreach (var song in playlist)
        {
            if (!File.Exists(song))
            {
                nonExistentSongs.Add(song);
            }
        }

        return new FindNonExistentSongsInPlaylistResultDto(nonExistentSongs);
    }
}
