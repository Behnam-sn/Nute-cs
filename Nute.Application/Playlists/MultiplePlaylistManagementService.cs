using Nute.Application.Playlists.Vms;
using Nute.Domain.Playlists;

namespace Nute.Application.Playlists;

public static class MultiplePlaylistManagementService
{
    public static ComparePlaylistsResultVm ComparePlaylists(string playlist1Path, string playlist2Path)
    {
        var playlist1 = Playlist.Parse(playlistPath: playlist1Path);
        var playlist2 = Playlist.Parse(playlistPath: playlist2Path);
        var result = playlist1.CompareTo(playlist2);

        return new ComparePlaylistsResultVm(
            Playlist1Title: playlist1.Title,
            Playlist1Songs: result.Playlist1UniqueItems.Select(i => i.SongPath),
            Playlist2Title: playlist2.Title,
            Playlist2Songs: result.Playlist2UniqueItems.Select(i => i.SongPath),
            InCommonSongs: result.InCommonItems.Select(i => i.SongPath)
        );
    }

    private static void ValidateSourcePath(string sourcePath)
    {
        if (!Directory.Exists(sourcePath))
        {
            throw new DirectoryNotFoundException($"{sourcePath} doesn't exists.");
        }
    }

    private static IEnumerable<T> TemplateMethod<T>(string sourcePath, Func<string, T> selector)
    {
        ValidateSourcePath(sourcePath: sourcePath);
        var playlists = Directory.EnumerateFiles(sourcePath, "*.m3u8");
        return playlists.Select(selector);
    }

    public static IEnumerable<GetNotFoundedSongsInPlaylistResultVm> GetNotFoundedSongsInAllPlaylists(string sourcePath)
    {
        return TemplateMethod(
            sourcePath: sourcePath,
            selector: SinglePlaylistManagementService.GetNotFoundedSongsInPlaylist
        );
    }

    public static IEnumerable<GetDuplicateSongsInPlaylistResultVm> GetDuplicateSongsInAllPlaylists(string sourcePath)
    {
        return TemplateMethod(
            sourcePath: sourcePath,
            selector: SinglePlaylistManagementService.GetDuplicateSongsInPlaylist
        );
    }

    public static IEnumerable<RemoveDuplicateSongsInPlaylistResultVm> RemoveDuplicateSongsInAllPlaylists(string sourcePath)
    {
        return TemplateMethod(
            sourcePath: sourcePath,
            selector: SinglePlaylistManagementService.RemoveDuplicateSongsInPlaylist
        );
    }

    public static IEnumerable<SortPlaylistResultVm> SortSongsInAllPlaylists(string sourcePath)
    {
        return TemplateMethod(
            sourcePath: sourcePath,
            selector: SinglePlaylistManagementService.SortSongsInPlaylist
        );
    }

    public static IEnumerable<UpdateSongsPathResultVm> ChangeSongsBasePathInAllPlaylists(
        string sourcePath,
        string currentBasePath,
        string currentBasePathType,
        string newBasePath,
        string newBasePathType,
        string destinationDirectoryPath
    )
    {
        return TemplateMethod(
            sourcePath: sourcePath,
            selector: playlistPath => SinglePlaylistManagementService.ChangeSongsBasePathInPlaylist(
                playlistPath: playlistPath,
                currentBasePath: currentBasePath,
                currentBasePathType: currentBasePathType,
                newBasePath: newBasePath,
                newBasePathType: newBasePathType,
                destinationDirectoryPath: destinationDirectoryPath
            )
        );
    }
}
