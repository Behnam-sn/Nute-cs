using Nute.Console.CommandControllers.Playlists;
using Nute.Console.CommandControllers.Songs;

namespace Nute.Console.CommandControllers;

internal class NuteCommandController : BaseCommandController
{
    protected override string Title { get; } = "Nute";

    internal NuteCommandController()
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
        new SinglePlaylistCommandController().Run();
    }

    private static void MultiplePlaylistManagement()
    {
        new MultiplePlaylistCommandController().Run();
    }

    private static void SongManagement()
    {
        new SongCommandController().Run();
    }
}
