namespace Nute.Domain;

public class PlaylistProcessor
{
    public static PlaylistProcessorRemoveDuplicateSongsInPlaylistResult RemoveDuplicateSongsInPlaylist(IEnumerable<string> playlist)
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

        return new PlaylistProcessorRemoveDuplicateSongsInPlaylistResult(songs, duplicateSongs);
    }

    public static IEnumerable<string> FindNonExistentSongsInPlaylist(IEnumerable<string> playlist)
    {
        var nonExistentSongs = new List<string>();

        foreach (var song in playlist)
        {
            if (!File.Exists(song))
            {
                nonExistentSongs.Add(song);
            }
        }

        return nonExistentSongs;
    }

    public static PlaylistProcessorComparePlaylistsResult ComparePlaylists(IEnumerable<string> playlist1, IEnumerable<string> playlist2)
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

        return new PlaylistProcessorComparePlaylistsResult(playlist1Songs, playlist2Songs, inCommonSongs);
    }
}
