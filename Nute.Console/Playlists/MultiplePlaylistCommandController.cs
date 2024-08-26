using Nute.Application.Playlists;

namespace Nute.Console.Playlists;

internal static class MultiplePlaylistCommandController
{
    private static readonly List<Command> _commands =
    [
        new(
            Commands:["Help", "H"],
            Action: PrintCommands
        ),
        new(
            Commands:["Exit", "E"],
            Action: () => {}
        ),
        new(
            Commands:["Get All Not Founded Songs", "GANFS"],
            Action: GetNotFoundedSongsInAllPlaylists
        ),
        new(
            Commands:["Get All Duplicate Songs", "GADS"],
            Action: GetDuplicateSongsInAllPlaylists
        ),
        new(
            Commands:["Remove All Duplicate Songs", "RADS"],
            Action: RemoveDuplicateSongsInAllPlaylists
        ),
        new(
            Commands:["Sort All Song", "SAS"],
            Action: SortSongsInAllPlaylists
        ),
        new(
            Commands:["Change All Songs Base Path", "CASBP"],
            Action: ChangeSongsBasePathInAllPlaylists
        ),
        new(
            Commands:["Compare Playlists", "CP"],
            Action: ComparePlaylists
        )
    ];

    internal static void Run()
    {
        while (true)
        {
            System.Console.Write("Playlist Command: ");
            var input = System.Console.ReadLine()?.ToLower();

            var command = _commands
                .FirstOrDefault(
                    i => i.Commands.Any(
                        j => j.Equals(input, StringComparison.CurrentCultureIgnoreCase)
                    )
                );

            if (command is null)
            {
                continue;
            }

            if (command.Commands.Contains("Exit"))
            {
                return;
            }

            command.Action();
        }
    }

    private static void PrintCommands()
    {
        foreach (var command in _commands)
        {
            foreach (var item in command.Commands)
            {
                System.Console.Write(item);
                System.Console.Write(", ");
            }
            System.Console.WriteLine("");
        }
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
