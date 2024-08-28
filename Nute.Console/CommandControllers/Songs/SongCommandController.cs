namespace Nute.Console.CommandControllers.Songs;

internal class SongCommandController : BaseCommandController
{
    internal SongCommandController()
    {
        _commands.AddRange([
            new(
                Commands: ["Compare Songs", "CS"],
                Action: CompareSongs
            ),
        ]);
    }

    private static void CompareSongs()
    {
        // var sourcePath = "";
        // var destinationPath = "";

        // try
        // {
        //     var result = SongsManagementService.CompareAllSongs(
        //        sourcePath: sourcePath,
        //        destinationPath: destinationPath
        //     );
        //     result.PrintInConsole();
        // }
        // catch (Exception exception)
        // {
        //     System.Console.WriteLine(exception);
        // }
    }
}
