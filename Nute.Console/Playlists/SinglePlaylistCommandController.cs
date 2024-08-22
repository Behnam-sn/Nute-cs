using Nute.Application.Playlists;

namespace Nute.Console.Playlists;

internal static class SinglePlaylistCommandController
{
    internal static void Run()
    {
        while (true)
        {
            System.Console.Write("Playlist Command: ");
            var command = System.Console.ReadLine()?.ToLower();

            switch (command)
            {
                case "help" or "h":
                    PrintCommands();
                    break;

                case "exit" or "e":
                    return;

                case "get not founded songs" or "gnfs":
                    GetNotFoundedSongsInPlaylist();
                    break;

                case "get duplicate songs" or "gds":
                    GetDuplicateSongsInPlaylist();
                    break;

                case "remove duplicate songs" or "rds":
                    RemoveDuplicateSongsInPlaylist();
                    break;

                case "sort songs" or "ss":
                    SortSongsInPlaylist();
                    break;

                case "change songs base path" or "csbp":
                    ChangeSongsBasePathInPlaylist();
                    break;
            }
        }
    }

    private static void PrintCommands()
    {
        System.Console.WriteLine("help or h");
        System.Console.WriteLine("exit or e");
        System.Console.WriteLine("Get Not Founded Songs or gnfs");
        System.Console.WriteLine("Get Duplicate Songs or gds");
        System.Console.WriteLine("Remove Duplicate Songs or rds");
        System.Console.WriteLine("Sort Songs or ss");
        System.Console.WriteLine("Change Songs Base Path or csbp");
        System.Console.WriteLine("");
    }

    private static void GetNotFoundedSongsInPlaylist()
    {
        //System.Console.Write("Playlist Path: ");
        //var playlistPath = System.Console.ReadLine();
        var playlistPath = "C:\\Users\\Behnam\\Music\\Playlists\\Chill.m3u8";

        SinglePlaylistManagementService
            .GetNotFoundedSongsInPlaylist(playlistPath: playlistPath)
            .PrintInConsole();
    }

    private static void GetDuplicateSongsInPlaylist()
    {
        //System.Console.Write("Playlist Path: ");
        //var playlistPath = System.Console.ReadLine();
        var playlistPath = "C:\\Users\\Behnam\\Music\\Playlists\\Chill.m3u8";

        SinglePlaylistManagementService
            .GetDuplicateSongsInPlaylist(playlistPath: playlistPath)
            .PrintInConsole();
    }

    private static void RemoveDuplicateSongsInPlaylist()
    {
        //System.Console.Write("Playlist Path: ");
        //var playlistPath = System.Console.ReadLine();
        var playlistPath = "C:\\Users\\Behnam\\Music\\Playlists\\Chill.m3u8";

        SinglePlaylistManagementService
            .RemoveDuplicateSongsInPlaylist(playlistPath: playlistPath)
            .PrintInConsole();
    }

    private static void SortSongsInPlaylist()
    {
        //System.Console.Write("Playlist Path: ");
        //var playlistPath = System.Console.ReadLine();
        var playlistPath = "C:\\Users\\Behnam\\Music\\Playlists\\Play On 2010s.m3u8";

        SinglePlaylistManagementService
            .SortSongsInPlaylist(playlistPath: playlistPath)
            .PrintInConsole();
    }

    private static void ChangeSongsBasePathInPlaylist()
    {
        //System.Console.Write("Playlist Path: ");
        //var playlistPath = System.Console.ReadLine();
        var playlistPath = "C:\\Users\\Behnam\\Music\\Playlists\\Play On 2010s.m3u8";
        var currentBasePath = "D:";
        var currentBasePathType = "Windows";
        var newBasePath = "/storage/emulated/0";
        var newBasePathType = "Linux";
        var destinationDirectoryPath = "D:\\Musics\\Playlist Converted";

        SinglePlaylistManagementService
            .ChangeSongsBasePathInPlaylist(
                playlistPath: playlistPath,
                currentBasePath: currentBasePath,
                currentBasePathType: currentBasePathType,
                newBasePath: newBasePath,
                newBasePathType: newBasePathType,
                destinationDirectoryPath: destinationDirectoryPath
            )
            .PrintInConsole();
    }
}
