using Nute.Application.Playlists.Vms;

namespace Nute.Application.Playlists;

public static class PlaylistsManagementService
{
    private static IEnumerable<T> TemplateMethod<T>(string sourcePath, Func<string, T> selector)
    {
        ValidateSourcePath(sourcePath: sourcePath);
        var playlists = Directory.EnumerateFiles(sourcePath, "*.m3u8");
        return playlists.Select(selector);
    }

    private static void ValidateSourcePath(string sourcePath)
    {
        if (!Directory.Exists(sourcePath))
        {
            throw new Exception($"{sourcePath} doesn't exists.");
        }
    }

    public static IEnumerable<GetNotFoundedSongsInPlaylistResultVm> GetAllNotFoundedSongs(string sourcePath)
    {
        return TemplateMethod(
            sourcePath: sourcePath,
            selector: PlaylistManagementService.GetNotFoundedSongs
        );
    }

    public static IEnumerable<GetDuplicateSongsInPlaylistResultVm> GetAllDuplicateSongs(string sourcePath)
    {
        return TemplateMethod(
            sourcePath: sourcePath,
            selector: PlaylistManagementService.GetDuplicateSongs
        );
    }

    public static IEnumerable<RemoveDuplicateSongsInPlaylistResultVm> RemoveAllDuplicateSongs(string sourcePath)
    {
        return TemplateMethod(
            sourcePath: sourcePath,
            selector: PlaylistManagementService.RemoveDuplicateSongs
        );
    }

    public static IEnumerable<SortPlaylistResultVm> SortAllPlaylists(string sourcePath)
    {
        return TemplateMethod(
            sourcePath: sourcePath,
            selector: PlaylistManagementService.Sort
        );
    }

    public static IEnumerable<UpdateSongsPathResultVm> AllUpdateSongsBasePath(
        string sourcePath,
        string oldBasePath,
        string newBasePath,
        bool isNewBasePathLinuxBased,
        string destinationDirectoryPath
    )
    {
        return TemplateMethod(
            sourcePath: sourcePath,
            selector: i => PlaylistManagementService.UpdateSongsBasePath(
                playlistPath: i,
                oldBasePath: oldBasePath,
                newBasePath: newBasePath,
                isNewBasePathLinuxBased: isNewBasePathLinuxBased,
                destinationDirectoryPath: destinationDirectoryPath
            )
        );
    }
}
