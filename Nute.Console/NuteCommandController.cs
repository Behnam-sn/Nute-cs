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

                case "playlist" or "p":
                    Playlist();
                    break;

                case "song" or "s":
                    Song();
                    break;

                case "exit" or "x":
                    return;
            }
        }
    }

    private static void PrintCommands()
    {
        System.Console.WriteLine("playlist, p");
        System.Console.WriteLine("help, h");
        System.Console.WriteLine("exit, x");
    }

    private static void Playlist()
    {
        PlaylistCommandController.Run();
    }

    private static void Song()
    {
        throw new NotImplementedException();
    }
}
