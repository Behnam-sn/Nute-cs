using Nute.Application;

namespace Nute.Console;

public class PlaylistCommandController
{
    public PlaylistCommandController()
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

                case "Remove Duplicate Songs In A Playlist" or "rm":
                    RemoveDuplicateSongsInPlaylist();
                    break;

                case "Compare Playlists" or "cp":
                    ComparePlaylists();
                    break;

                case "exit" or "x":
                    return;
            }
        }
    }

    private static void PrintCommands()
    {
        System.Console.WriteLine("Remove Duplicate Songs In A Playlist, rm");
        System.Console.WriteLine("ComparePlaylists, cp");
        System.Console.WriteLine("help, h");
        System.Console.WriteLine("exit, x");
    }

    private static void RemoveDuplicateSongsInPlaylist()
    {
        throw new NotImplementedException();
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

            result.ShowInConsole();
        }
        catch (Exception exception)
        {
            System.Console.WriteLine(exception.Message);
        }
    }
}
