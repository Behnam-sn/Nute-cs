namespace Nute.Application;

public class PlaylistManagementService
{
    public static ComparePlaylistsResultVm ComparePlaylists(string playlistPath1, string playlistPath2)
    {
        var result = new ComparePlaylistsResultVm();
        // only in playlist 1
        // only in playlist 2
        // in both
        return result;
    }

    public static RemoveDuplicateSongsInPlaylistResultVm RemoveDuplicateSongsInPlaylist(string playlistPath)
    {
        var result = new RemoveDuplicateSongsInPlaylistResultVm();
        // open playlist file
        // read each song
        // add each song to a hashset
        // return the hashset
        // keep the duplicates
        // return them
        // update the playlist file
        return result;
    }

    public static FindNonExistentSongsInPlaylistResultVm FindNonExistentSongsInPlaylist(string playlistPath)
    {
        var result = new FindNonExistentSongsInPlaylistResultVm();
        // open playlist file
        // turn song into a list
        // find non existent song
        // return them 
        // show them

        return result;
    }

    public static SortPlayListResultVm SortPlayList(string playlistPath)
    {
        var result = new SortPlayListResultVm();
        return result;
    }
}
