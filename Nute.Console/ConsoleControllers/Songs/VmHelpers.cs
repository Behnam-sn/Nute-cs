using Nute.Application.Songs.Vms;

namespace Nute.Console.ConsoleControllers.Songs;

internal static class VmHelpers
{
    internal static void PrintInConsole(this CompareSongsResultVm vm)
    {
    }

    internal static void PrintInConsole(this OrganizeSongsByAlbumResponse response)
    {
        System.Console.WriteLine(response.name);
    }
}
