using Nute.Application.Vms;

namespace Nute.Application;

public class PlaylistManagementService
{
    public static ComparePlaylistsResultVm ComparePlaylists(string playlistPath1, string playlistPath2)
    {
        // only in playlist 1
        // only in playlist 2
        // in both
        return new ComparePlaylistsResultVm();
    }

    public static RemoveDuplicateSongsInPlaylistResultVm RemoveDuplicateSongsInPlaylist(string playlistPath)
    {
        // open playlist file
        // read each song
        // add each song to a hashset
        // return the hashset
        // keep the duplicates
        // return them
        // update the playlist file
        return new RemoveDuplicateSongsInPlaylistResultVm();
    }

    public static FindNonExistentSongsInPlaylistResultVm FindNonExistentSongsInPlaylist(string playlistPath)
    {
        // open playlist file
        // turn song into a list
        // find non existent song
        // return them 
        // show them
        return new FindNonExistentSongsInPlaylistResultVm();
    }

    public static SortPlayListResultVm SortPlaylist(string playlistPath)
    {
        return new SortPlayListResultVm();
    }

    public static void AdaptPlaylistForAndroid(string playlistPath)
    {

    }
}
