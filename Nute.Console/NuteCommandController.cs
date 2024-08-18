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

                case "playlist" or "p":
                    Playlist();
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
        System.Console.WriteLine("Playlist or p");
        System.Console.WriteLine("Song or s");
    }

    private static void Playlist()
    {
        PlaylistCommandController.Run();
    }

    private static void Song()
    {
        SongCommandController.Run();
    }
}
