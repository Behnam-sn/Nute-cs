namespace Nute.Application;

public class PlaylistManagementService
{
    public RemoveDuplicateSongsInPlaylistResult Execute(RemoveDuplicateSongsInPlaylistCommand command)
    {
        var result = new RemoveDuplicateSongsInPlaylistResult();
        // open playlist file
        // read each line
        // add each song to a hashset
        return result;
    }

    public FindNonExistentSongsInPlaylistResult Execute(FindNonExistentSongsInPlaylistCommand command)
    {
        var result = new FindNonExistentSongsInPlaylistResult();
        return result;
    }

    public ComparePlaylistsResult Execute(ComparePlaylistsCommand command)
    {
        var result = new ComparePlaylistsResult();
        return result;
    }

    public SortPlayListResult Execute(SortPlayListCommand command)
    {
        var result = new SortPlayListResult();
        return result;
    }
}
