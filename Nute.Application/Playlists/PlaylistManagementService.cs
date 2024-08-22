using Nute.Application.Playlists.Vms;
using Nute.Domain.Playlists;
using Nute.Domain.Playlists.Enums;

namespace Nute.Application.Playlists;

public static class PlaylistManagementService
{
    #region Single Playlist Management

    public static GetNotFoundedSongsInPlaylistResultVm GetNotFoundedSongsInPlaylist(string playlistPath)
    {
        var playlist = Playlist.Parse(playlistPath: playlistPath);
        var notFoundedSongs = playlist.GetNotFoundedItems();

        return new GetNotFoundedSongsInPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            NotFoundedSongs: notFoundedSongs.Select(i => i.Path)
        );
    }

    public static GetDuplicateSongsInPlaylistResultVm GetDuplicateSongsInPlaylist(string playlistPath)
    {
        var playlist = Playlist.Parse(playlistPath: playlistPath);
        var duplicateSongs = playlist.GetDuplicateItems();

        return new GetDuplicateSongsInPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            DuplicateSongs: duplicateSongs.Select(i => i.Path)
        );
    }

    public static RemoveDuplicateSongsInPlaylistResultVm RemoveDuplicateSongsInPlaylist(string playlistPath)
    {
        var playlist = Playlist.Parse(playlistPath: playlistPath);
        var duplicateSongs = playlist.GetDuplicateItems();
        playlist.RemoveDuplicateItems();
        playlist.Save();

        return new RemoveDuplicateSongsInPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            DuplicateSongs: duplicateSongs.Select(i => i.Path)
        );
    }

    public static SortPlaylistResultVm SortSongsInPlaylist(string playlistPath)
    {
        var playlist = Playlist.Parse(playlistPath: playlistPath);
        playlist.SortItems();
        playlist.Save();

        return new SortPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            SortedSongs: playlist.Items.Select(i => i.Path)
        );
    }

    public static UpdateSongsPathResultVm ChangeSongsBasePathInPlaylist(
        string playlistPath,
        string currentBasePath,
        string currentBasePathType,
        string newBasePath,
        string newBasePathType,
        string destinationDirectoryPath
    )
    {
        var playlist = Playlist.Parse(playlistPath: playlistPath);
        playlist.UpdateItemsBasePath(
            currentBasePath: currentBasePath,
            currentBasePathType: Enum.Parse<PathTypes>(currentBasePathType),
            newBasePath: newBasePath,
            newBasePathType: Enum.Parse<PathTypes>(newBasePathType)
        );
        playlist.Save(destinationDirectoryPath: destinationDirectoryPath);

        return new UpdateSongsPathResultVm(
            PlaylistTitle: playlist.Title
        );
    }

    #endregion

    #region Multiple Playlist Management

    public static ComparePlaylistsResultVm ComparePlaylists(string playlist1Path, string playlist2Path)
    {
        var playlist1 = Playlist.Parse(playlistPath: playlist1Path);
        var playlist2 = Playlist.Parse(playlistPath: playlist2Path);
        var result = playlist1.CompareTo(playlist2);

        return new ComparePlaylistsResultVm(
            Playlist1Title: playlist1.Title,
            Playlist1Songs: result.Playlist1UniqueItems.Select(i => i.Path),
            Playlist2Title: playlist2.Title,
            Playlist2Songs: result.Playlist2UniqueItems.Select(i => i.Path),
            InCommonSongs: result.InCommonItems.Select(i => i.Path)
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
            selector: GetNotFoundedSongsInPlaylist
        );
    }

    public static IEnumerable<GetDuplicateSongsInPlaylistResultVm> GetDuplicateSongsInAllPlaylists(string sourcePath)
    {
        return TemplateMethod(
            sourcePath: sourcePath,
            selector: GetDuplicateSongsInPlaylist
        );
    }

    public static IEnumerable<RemoveDuplicateSongsInPlaylistResultVm> RemoveDuplicateSongsInAllPlaylists(string sourcePath)
    {
        return TemplateMethod(
            sourcePath: sourcePath,
            selector: RemoveDuplicateSongsInPlaylist
        );
    }

    public static IEnumerable<SortPlaylistResultVm> SortSongsInAllPlaylists(string sourcePath)
    {
        return TemplateMethod(
            sourcePath: sourcePath,
            selector: SortSongsInPlaylist
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
            selector: i => ChangeSongsBasePathInPlaylist(
                playlistPath: i,
                currentBasePath: currentBasePath,
                currentBasePathType: currentBasePathType,
                newBasePath: newBasePath,
                newBasePathType: newBasePathType,
                destinationDirectoryPath: destinationDirectoryPath
            )
        );
    }

    #endregion
}
