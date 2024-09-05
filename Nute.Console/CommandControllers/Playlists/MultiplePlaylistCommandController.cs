using Nute.Application.Playlists;

namespace Nute.Console.CommandControllers.Playlists;

internal class MultiplePlaylistCommandController : BaseCommandController
{
    protected override string Title { get; } = "Multiple Playlist";

    internal MultiplePlaylistCommandController()
    {
        Commands.AddRange([
            new(
                Titles: ["Get All Not Founded Songs", "GANFS"],
                Action: GetNotFoundedSongsInAllPlaylists
            ),
            new(
                Titles: ["Get All Duplicate Songs", "GADS"],
                Action: GetDuplicateSongsInAllPlaylists
            ),
            new(
                Titles: ["Remove All Duplicate Songs", "RADS"],
                Action: RemoveDuplicateSongsInAllPlaylists
            ),
            new(
                Titles: ["Sort All Song", "SAS"],
                Action: SortSongsInAllPlaylists
            ),
            new(
                Titles: ["Change All Songs Base Path", "CASBP"],
                Action: ChangeSongsBasePathInAllPlaylists
            ),
            new(
                Titles: ["Compare Playlists", "CP"],
                Action: ComparePlaylists
            )
        ]);
    }

    private static void GetNotFoundedSongsInAllPlaylists()
    {
        //System.Console.Write("Playlist Path: ");
        //var sourcePath = System.Console.ReadLine();
        var sourcePath = "C:\\Users\\Behnam\\Music\\Playlists";

        MultiplePlaylistManagementService
            .GetNotFoundedSongsInAllPlaylists(sourcePath: sourcePath)
            .PrintInConsole();
    }

    private static void GetDuplicateSongsInAllPlaylists()
    {
        //System.Console.Write("Playlist Path: ");
        //var sourcePath = System.Console.ReadLine();
        var sourcePath = "C:\\Users\\Behnam\\Music\\Playlists";

        MultiplePlaylistManagementService
            .GetDuplicateSongsInAllPlaylists(sourcePath: sourcePath)
            .PrintInConsole();
    }

    private static void RemoveDuplicateSongsInAllPlaylists()
    {
        //System.Console.Write("Playlist Path: ");
        //var sourcePath = System.Console.ReadLine();
        var sourcePath = "C:\\Users\\Behnam\\Music\\Playlists";

        MultiplePlaylistManagementService
            .RemoveDuplicateSongsInAllPlaylists(sourcePath: sourcePath)
            .PrintInConsole();
    }

    private static void SortSongsInAllPlaylists()
    {
        //System.Console.Write("Playlist Path: ");
        //var playlistPath = System.Console.ReadLine();
        var sourcePath = "C:\\Users\\Behnam\\Music\\Playlists";

        MultiplePlaylistManagementService
            .SortSongsInAllPlaylists(sourcePath: sourcePath)
            .PrintInConsole();
    }

    private static void ChangeSongsBasePathInAllPlaylists()
    {
        //System.Console.Write("Playlist Path: ");
        //var playlistPath = System.Console.ReadLine();
        var sourcePath = "C:\\Users\\Behnam\\Music\\Playlists";
        var currentBasePath = "D:";
        var currentBasePathType = "Windows";
        var newBasePath = "/storage/emulated/0";
        var newBasePathType = "Linux";
        var destinationDirectoryPath = "D:\\Musics\\Playlist Converted";

        MultiplePlaylistManagementService
            .ChangeSongsBasePathInAllPlaylists(
                sourcePath: sourcePath,
                currentBasePath: currentBasePath,
                currentBasePathType: currentBasePathType,
                newBasePath: newBasePath,
                newBasePathType: newBasePathType,
                destinationDirectoryPath: destinationDirectoryPath
            )
            .PrintInConsole();
    }

    private static void ComparePlaylists()
    {
        //System.Console.Write("Playlist 1 Path: ");
        //var playlist1Path = System.Console.ReadLine();
        //System.Console.Write("Playlist 2 Path: ");
        //var playlist2Path = System.Console.ReadLine();
        var playlist1Path = "C:\\Users\\Behnam\\Music\\Playlists\\Persian.m3u8";
        var playlist2Path = "C:\\Users\\Behnam\\Music\\Playlists\\Family Friendly.m3u8";

        MultiplePlaylistManagementService
            .ComparePlaylists(
                playlist1Path: playlist1Path,
                playlist2Path: playlist2Path
            )
            .PrintInConsole();
    }
}
