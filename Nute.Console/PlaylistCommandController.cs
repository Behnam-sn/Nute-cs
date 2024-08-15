using Nute.Application;

namespace Nute.Console;

internal static class PlaylistCommandController
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

                case "exit" or "x":
                    return;

                case "Get Not Founded Songs" or "gnfs":
                    GetNotFoundedSongsInPlaylist();
                    break;

                case "Get Duplicate Songs" or "gds":
                    GetDuplicateSongsInPlaylist();
                    break;

                case "Remove Duplicate Songs" or "rds":
                    RemoveDuplicateSongsInPlaylist();
                    break;

                case "Compare Playlists" or "cp":
                    ComparePlaylists();
                    break;
            }
        }
    }

    private static void PrintCommands()
    {
        System.Console.WriteLine("Get Not Founded Songs or gnfs");
        System.Console.WriteLine("Get Duplicate Songs or gds");
        System.Console.WriteLine("Remove Duplicate Songs or rds");
        System.Console.WriteLine("Compare Playlists, cp");
        System.Console.WriteLine("help, h");
        System.Console.WriteLine("exit, x");
    }

    private static void GetNotFoundedSongsInPlaylist()
    {
        //System.Console.Write("Playlist Path: ");
        //var playlistPath = System.Console.ReadLine();
        var playlistPath = "C:\\Users\\Behnam\\Music\\Playlists\\Chill.m3u8";

        try
        {
            var result = PlaylistsManagementService.GetNotFoundedSongsInPlaylist(playlistPath: playlistPath);
            result.PrintInConsole();
        }
        catch (Exception exception)
        {
            System.Console.WriteLine(exception.Message);
        }
    }

    private static void GetDuplicateSongsInPlaylist()
    {
        //System.Console.Write("Playlist Path: ");
        //var playlistPath = System.Console.ReadLine();
        var playlistPath = "C:\\Users\\Behnam\\Music\\Playlists\\Chill.m3u8";

        try
        {
            var result = PlaylistsManagementService.GetDuplicateSongsInPlaylist(playlistPath: playlistPath);
            result.PrintInConsole();
        }
        catch (Exception exception)
        {
            System.Console.WriteLine(exception.Message);
        }
    }

    private static void RemoveDuplicateSongsInPlaylist()
    {
        //System.Console.Write("Playlist Path: ");
        //var playlistPath = System.Console.ReadLine();
        var playlistPath = "C:\\Users\\Behnam\\Music\\Playlists\\Chill.m3u8";

        try
        {
            var result = PlaylistsManagementService.RemoveDuplicateSongsInPlaylist(playlistPath: playlistPath);
            result.PrintInConsole();
        }
        catch (Exception exception)
        {
            System.Console.WriteLine(exception.Message);
        }
    }

    private static void ComparePlaylists()
    {
        System.Console.Write("Playlist 1 Path: ");
        var playlist1Path = System.Console.ReadLine();
        System.Console.Write("Playlist 2 Path: ");
        var playlist2Path = System.Console.ReadLine();

        try
        {
            var result = PlaylistsManagementService.ComparePlaylists(
                playlist1Path: playlist1Path,
                playlist2Path: playlist2Path);

            result.PrintInConsole();
        }
        catch (Exception exception)
        {
            System.Console.WriteLine(exception.Message);
        }
    }
}
