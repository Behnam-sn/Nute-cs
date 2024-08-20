using Nute.Application.Playlists;

namespace Nute.Console.Playlists;

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

                case "exit" or "e":
                    return;

                case "get all not founded songs" or "ganfs":
                    GetAllNotFoundedSongsInPlaylist();
                    break;

                case "get all duplicate songs" or "gads":
                    GetAllDuplicateSongsInPlaylist();
                    break;

                case "get not founded songs" or "gnfs":
                    GetNotFoundedSongsInPlaylist();
                    break;

                case "get duplicate songs" or "gds":
                    GetDuplicateSongsInPlaylist();
                    break;

                case "remove duplicate songs" or "rds":
                    RemoveDuplicateSongsInPlaylist();
                    break;

                case "compare playlists" or "cp":
                    ComparePlaylists();
                    break;

                case "sort" or "s":
                    SortPlaylist();
                    break;

                case "update songs base path" or "usbp":
                    UpdateSongsBasePath();
                    break;
            }
        }
    }

    private static void PrintCommands()
    {
        System.Console.WriteLine("help or h");
        System.Console.WriteLine("exit or e");
        System.Console.WriteLine("Get All Not Founded Songs or ganfs");
        System.Console.WriteLine("Get All Duplicate Songs or gads");
        System.Console.WriteLine("Get Not Founded Songs or gnfs");
        System.Console.WriteLine("Get Duplicate Songs or gds");
        System.Console.WriteLine("Remove Duplicate Songs or rds");
        System.Console.WriteLine("Compare Playlists or cp");
        System.Console.WriteLine("Sort or s");
        System.Console.WriteLine("Update Songs Base Path or usbp");
        System.Console.WriteLine("");
    }

    private static void GetAllNotFoundedSongsInPlaylist()
    {
        //System.Console.Write("Playlist Path: ");
        //var sourcePath = System.Console.ReadLine();
        var sourcePath = "C:\\Users\\Behnam\\Music\\Playlists";

        try
        {
            var result = PlaylistManagementService.GetAllNotFoundedSongs(sourcePath: sourcePath);
            foreach (var item in result)
            {
                item.PrintInConsole();
            }
        }
        catch (Exception exception)
        {
            System.Console.WriteLine(exception.Message);
        }
    }

    private static void GetAllDuplicateSongsInPlaylist()
    {
        //System.Console.Write("Playlist Path: ");
        //var sourcePath = System.Console.ReadLine();
        var sourcePath = "C:\\Users\\Behnam\\Music\\Playlists";

        try
        {
            var result = PlaylistManagementService.GetAllDuplicateSongs(sourcePath: sourcePath);
            foreach (var item in result)
            {
                item.PrintInConsole();
            }
        }
        catch (Exception exception)
        {
            System.Console.WriteLine(exception.Message);
        }
    }

    private static void GetNotFoundedSongsInPlaylist()
    {
        //System.Console.Write("Playlist Path: ");
        //var playlistPath = System.Console.ReadLine();
        var playlistPath = "C:\\Users\\Behnam\\Music\\Playlists\\Chill.m3u8";

        try
        {
            var result = PlaylistManagementService.GetNotFoundedSongs(playlistPath: playlistPath);
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
            var result = PlaylistManagementService.GetDuplicateSongs(playlistPath: playlistPath);
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
            var result = PlaylistManagementService.RemoveDuplicateSongs(playlistPath: playlistPath);
            result.PrintInConsole();
        }
        catch (Exception exception)
        {
            System.Console.WriteLine(exception.Message);
        }
    }

    private static void ComparePlaylists()
    {
        //System.Console.Write("Playlist 1 Path: ");
        //var playlist1Path = System.Console.ReadLine();
        //System.Console.Write("Playlist 2 Path: ");
        //var playlist2Path = System.Console.ReadLine();
        var playlist1Path = "C:\\Users\\Behnam\\Music\\Playlists\\Persian.m3u8";
        var playlist2Path = "C:\\Users\\Behnam\\Music\\Playlists\\Family Friendly.m3u8";

        try
        {
            var result = PlaylistManagementService.Compare(
                playlist1Path: playlist1Path,
                playlist2Path: playlist2Path);

            result.PrintInConsole();
        }
        catch (Exception exception)
        {
            System.Console.WriteLine(exception.Message);
        }
    }

    private static void SortPlaylist()
    {
        //System.Console.Write("Playlist Path: ");
        //var playlistPath = System.Console.ReadLine();
        var playlistPath = "C:\\Users\\Behnam\\Music\\Playlists\\Play On 2010s.m3u8";

        try
        {
            var result = PlaylistManagementService.Sort(playlistPath: playlistPath);
            result.PrintInConsole();
        }
        catch (Exception exception)
        {
            System.Console.WriteLine(exception.Message);
        }
    }

    private static void UpdateSongsBasePath()
    {
        //System.Console.Write("Playlist Path: ");
        //var playlistPath = System.Console.ReadLine();
        var playlistPath = "C:\\Users\\Behnam\\Music\\Playlists\\Play On 2010s.m3u8";
        var oldBasePath = "D:";
        var newBasePath = "/storage/emulated/0";
        var destinationDirectoryPath = "D:\\Musics\\Playlist Converted";

        try
        {
            var result = PlaylistManagementService.UpdateSongsBasePath(
                playlistPath: playlistPath,
                oldBasePath: oldBasePath,
                newBasePath: newBasePath,
                isNewBasePathLinuxBased: true,
                destinationDirectoryPath: destinationDirectoryPath
            );
            result.PrintInConsole();
        }
        catch (Exception exception)
        {
            System.Console.WriteLine(exception.Message);
        }
    }
}
