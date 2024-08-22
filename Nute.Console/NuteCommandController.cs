using Nute.Console.Playlists;
using Nute.Console.Songs;

namespace Nute.Console;

internal static class NuteCommandController
{
    internal static void Run()
    {
        while (true)
        {
            System.Console.Write("Command: ");
            var command = System.Console.ReadLine()?.ToLower();

            switch (command)
            {
                case "help" or "h":
                    PrintCommands();
                    break;

                case "exit" or "e":
                    return;

                case "single playlist management" or "spm":
                    SinglePlaylistManagement();
                    break;

                case "multiple playlist management" or "mpm":
                    MultiplePlaylistManagement();
                    break;

                case "song" or "s":
                    Song();
                    break;
            }
        }
    }

    private static void PrintCommands()
    {
        System.Console.WriteLine("Help or h");
        System.Console.WriteLine("Exit or e");
        System.Console.WriteLine("Single Playlist Management or spm");
        System.Console.WriteLine("Multiple Playlist Management or mpm");
        System.Console.WriteLine("Song or s");
    }

    private static void SinglePlaylistManagement()
    {
        SinglePlaylistCommandController.Run();
    }

    private static void MultiplePlaylistManagement()
    {
        MultiplePlaylistCommandController.Run();
    }

    private static void Song()
    {
        SongCommandController.Run();
    }
}
