namespace Nute.Domain;

public class PlaylistProcessor
{
    public static RemoveDuplicateSongsInPlaylistResult RemoveDuplicateSongsInPlaylist(IEnumerable<string> playlist)
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

        return new RemoveDuplicateSongsInPlaylistResult(songs, duplicateSongs);
    }

    public static FindNonExistentSongsInPlaylistResult FindNonExistentSongsInPlaylist(IEnumerable<string> playlist)
    {
        var nonExistentSongs = new List<string>();

        foreach (var song in playlist)
        {
            if (!File.Exists(song))
            {
                nonExistentSongs.Add(song);
            }
        }

        return new FindNonExistentSongsInPlaylistResult(nonExistentSongs);
    }

    public static ComparePlaylistsResult ComparePlaylists(IEnumerable<string> playlist1, IEnumerable<string> playlist2)
    {
        var inCommonSongs = new List<string>();

        foreach (var song in playlist1)
        {
            if (playlist2.Contains(song))
            {
                inCommonSongs.Add(song);
            }
        }

        var playlist1Songs = playlist1.ToList();
        playlist1Songs.RemoveAll(i => inCommonSongs.Contains(i));

        var playlist2Songs = playlist2.ToList();
        playlist2Songs.RemoveAll(i => inCommonSongs.Contains(i));

        return new ComparePlaylistsResult(playlist1Songs, playlist2Songs, inCommonSongs);
    }
}
