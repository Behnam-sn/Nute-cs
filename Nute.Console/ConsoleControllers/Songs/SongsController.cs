using Nute.Application.Songs;

namespace Nute.Console.ConsoleControllers.Songs;

internal class SongsController : BaseController
{
    protected override string Title { get; } = "Songs";

    internal SongsController()
    {
        Commands.AddRange([
            new(Titles: ["Compare Songs", "CS"], Action: CompareSongs),
            new(Titles: ["Organize Songs By Album", "OSBA"], Action: OrganizeSongsByAlbum),
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

    private static void OrganizeSongsByAlbum()
    {
        System.Console.Write("Path: ");
        var path = System.Console.ReadLine();

        var result = SongsManagementService.OrganizeSongsByAlbum(path);
        foreach (var item in result)
        {
            item.PrintInConsole();
        }
    }
}
