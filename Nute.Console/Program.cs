namespace Nute.Console;

public static class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            System.Console.Write("Command: ");
            var command = System.Console.ReadLine()?.ToLower();

            switch (command)
            {
                case "help" or "h":
                    PlaylistCommandController.PrintCommands();
                    break;

                case "playlist" or "p":
                    PlaylistCommandController.Playlist();
                    break;

                case "exit" or "x":
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
