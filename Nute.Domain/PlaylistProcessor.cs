namespace Nute.Domain;

public class PlaylistProcessor
{
    public PlaylistProcessorRemoveDuplicateSongsInPlaylistResult RemoveDuplicateSongsInPlaylist(IEnumerable<string> playlist)
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
}