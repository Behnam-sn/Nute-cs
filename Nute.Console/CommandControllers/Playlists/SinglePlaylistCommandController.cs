using Nute.Application.Playlists;

namespace Nute.Console.CommandControllers.Playlists;

internal class SinglePlaylistCommandController : BaseCommandController
{
    protected override string Title { get; } = "Single Playlist";

    internal SinglePlaylistCommandController()
    {
        Commands.AddRange([
            new(
                Commands: ["Get Not Founded Songs", "GNFS"],
                Action: GetNotFoundedSongsInPlaylist
            ),
            new(
                Commands: ["Get Duplicate Songs", "GDS"],
                Action: GetDuplicateSongsInPlaylist
            ),
            new(
                Commands: ["Remove Duplicate Songs", "RDS"],
                Action: RemoveDuplicateSongsInPlaylist
            ),
            new(
                Commands: ["Sort Songs", "SS"],
                Action: SortSongsInPlaylist
            ),
            new(
                Commands: ["Change Songs Base Path", "CSBP"],
                Action: ChangeSongsBasePathInPlaylist
            )
        ]);
    }

    private static void GetNotFoundedSongsInPlaylist()
    {
        //System.Console.Write("Playlist Path: ");
        //var playlistPath = System.Console.ReadLine();
        var playlistPath = "C:\\Users\\Behnam\\Music\\Playlists\\Chill.m3u8";

        SinglePlaylistManagementService
            .GetNotFoundedSongsInPlaylist(playlistPath: playlistPath)
            .PrintInConsole();
    }

    private static void GetDuplicateSongsInPlaylist()
    {
        //System.Console.Write("Playlist Path: ");
        //var playlistPath = System.Console.ReadLine();
        var playlistPath = "C:\\Users\\Behnam\\Music\\Playlists\\Chill.m3u8";

        SinglePlaylistManagementService
            .GetDuplicateSongsInPlaylist(playlistPath: playlistPath)
            .PrintInConsole();
    }

    private static void RemoveDuplicateSongsInPlaylist()
    {
        //System.Console.Write("Playlist Path: ");
        //var playlistPath = System.Console.ReadLine();
        var playlistPath = "C:\\Users\\Behnam\\Music\\Playlists\\Chill.m3u8";

        SinglePlaylistManagementService
            .RemoveDuplicateSongsInPlaylist(playlistPath: playlistPath)
            .PrintInConsole();
    }

    private static void SortSongsInPlaylist()
    {
        //System.Console.Write("Playlist Path: ");
        //var playlistPath = System.Console.ReadLine();
        var playlistPath = "C:\\Users\\Behnam\\Music\\Playlists\\Play On 2010s.m3u8";

        SinglePlaylistManagementService
            .SortSongsInPlaylist(playlistPath: playlistPath)
            .PrintInConsole();
    }

    private static void ChangeSongsBasePathInPlaylist()
    {
        //System.Console.Write("Playlist Path: ");
        //var playlistPath = System.Console.ReadLine();
        var playlistPath = "C:\\Users\\Behnam\\Music\\Playlists\\Play On 2010s.m3u8";
        var currentBasePath = "D:";
        var currentBasePathType = "Windows";
        var newBasePath = "/storage/emulated/0";
        var newBasePathType = "Linux";
        var destinationDirectoryPath = "D:\\Musics\\Playlist Converted";

        SinglePlaylistManagementService
            .ChangeSongsBasePathInPlaylist(
                playlistPath: playlistPath,
                currentBasePath: currentBasePath,
                currentBasePathType: currentBasePathType,
                newBasePath: newBasePath,
                newBasePathType: newBasePathType,
                destinationDirectoryPath: destinationDirectoryPath
            )
            .PrintInConsole();
    }
}
