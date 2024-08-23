using Nute.Application.Playlists;

namespace Nute.Console.Playlists;

internal static class MultiplePlaylistCommandController
{
    private static readonly List<Command> availableCommands =
    [
        new(["Help", "H"], PrintCommands),
        new(["Get All Not Founded Songs", "GANFS"], GetNotFoundedSongsInAllPlaylists)
    ];

    internal static void Run()
    {
        while (true)
        {
            System.Console.Write("Playlist Command: ");
            var command = System.Console.ReadLine()?.ToLower();

            availableCommands
                .FirstOrDefault(
                    i => i.Commands.Any(
                        j => j.Equals(command, StringComparison.CurrentCultureIgnoreCase)
                    )
                )
                ?.Action();

            // switch (command)
            // {
            //     case "help" or "h":
            //         PrintCommands();
            //         break;

            //     case "exit" or "e":
            //         return;

            //     case "get all not founded songs" or "ganfs":
            //         GetNotFoundedSongsInAllPlaylists();
            //         break;

            //     case "get all duplicate songs" or "gads":
            //         GetDuplicateSongsInAllPlaylists();
            //         break;

            //     case "remove all duplicate songs" or "rads":
            //         RemoveDuplicateSongsInAllPlaylists();
            //         break;

            //     case "sort all song" or "sas":
            //         SortSongsInAllPlaylists();
            //         break;

            //     case "change all songs base path" or "casbp":
            //         ChangeSongsBasePathInAllPlaylists();
            //         break;

            //     case "compare playlists" or "cp":
            //         ComparePlaylists();
            //         break;
            // }
        }
    }

    private static void PrintCommands()
    {
        System.Console.WriteLine("help or h");
        System.Console.WriteLine("exit or e");
        System.Console.WriteLine("Get All Not Founded Songs or ganfs");
        System.Console.WriteLine("Get All Duplicate Songs or gads");
        System.Console.WriteLine("Remove All Duplicate Songs or rads");
        System.Console.WriteLine("Sort All Song or sas");
        System.Console.WriteLine("Change All Songs Base Path or casbp");
        System.Console.WriteLine("Compare Playlists or cp");
        System.Console.WriteLine("");
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
