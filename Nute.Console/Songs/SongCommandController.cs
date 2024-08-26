namespace Nute.Console.Songs;

internal static class SongCommandController
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

                case "exit" or "x":
                    return;

                case "playlist" or "p":
                    CompareSongs();
                    break;
            }
        }
    }

    private static void PrintCommands()
    {
        System.Console.WriteLine("Help or h");
        System.Console.WriteLine("Exit or e");
        System.Console.WriteLine("Playlist, p");
        System.Console.WriteLine("Song or s");
    }

    private static void CompareSongs()
    {
        var sourcePath = "";
        var destinationPath = "";

        try
        {
            //var result = SongsManagementService.CompareAllSongs(
            //    sourcePath: sourcePath,
            //    destinationPath: destinationPath
            //);
            //result.PrintInConsole();
        }
        catch (Exception exception)
        {
            System.Console.WriteLine(exception);
        }
    }
}
