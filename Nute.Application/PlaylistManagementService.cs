namespace Nute.Application;

public class PlaylistManagementService
{
    public RemoveDuplicateSongsInPlaylistResultVm Execute(RemoveDuplicateSongsInPlaylistCommand command)
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

    public FindNonExistentSongsInPlaylistResultVm Execute(FindNonExistentSongsInPlaylistCommand command)
    {
        var result = new FindNonExistentSongsInPlaylistResultVm();
        // open playlist file
        // turn song into a list
        // find non existent song
        // return them 
        // show them

        return result;
    }

    public ComparePlaylistsResult Execute(ComparePlaylistsCommand command)
    {
        var result = new ComparePlaylistsResult();
        // only in playlist 1
        // only in playlist 2
        // in both
        return result;
    }

    public SortPlayListResult Execute(SortPlayListCommand command)
    {
        var result = new SortPlayListResult();
        return result;
    }
}
