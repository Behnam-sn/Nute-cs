using Nute.Application.Songs.Vms;

namespace Nute.Application.Songs;

public static class SongsManagementService
{
    public static CompareSongsResultVm CompareSongs(string sourcePath, string destinationPath)
    {
        return new CompareSongsResultVm();
    }

    public static void SyncSongs()
    {
    }
}
