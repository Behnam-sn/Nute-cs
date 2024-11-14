using Nute.Console.ConsoleControllers.Playlists;
using Nute.Console.ConsoleControllers.Songs;

namespace Nute.Console.ConsoleControllers;

internal class MainController : BaseController
{
    protected override string Title { get; } = "Nute";

    internal MainController()
    {
        Commands.AddRange([
            new(
                Titles: ["Single Playlist Management", "SPM"],
                Action: SinglePlaylistManagement
            ),
            new(
                Titles: ["Multiple Playlist Management", "MPM"],
                Action: MultiplePlaylistManagement
            ),
            new(
                Titles: ["Song", "S"],
                Action: SongManagement
            ),
        ]);
    }

    private static void SinglePlaylistManagement()
    {
        new SinglePlaylistController().Run();
    }

    private static void MultiplePlaylistManagement()
    {
        new MultiplePlaylistController().Run();
    }

    private static void SongManagement()
    {
        new SongsController().Run();
    }
}
