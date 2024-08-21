using Nute.Application.Playlists.Vms;
using Nute.Domain.Playlists;

namespace Nute.Application.Playlists;

public static class PlaylistManagementService
{
    public static GetNotFoundedSongsInPlaylistResultVm GetNotFoundedSongs(string playlistPath)
    {
        var playlist = Playlist.Parse(playlistPath: playlistPath);
        var notFoundedSongs = playlist.GetNotFoundedItems();

        return new GetNotFoundedSongsInPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            NotFoundedSongs: notFoundedSongs.Select(i => i.Path)
        );
    }

    public static GetDuplicateSongsInPlaylistResultVm GetDuplicateSongs(string playlistPath)
    {
        var playlist = Playlist.Parse(playlistPath: playlistPath);
        var duplicateSongs = playlist.GetDuplicateItems();

        return new GetDuplicateSongsInPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            DuplicateSongs: duplicateSongs.Select(i => i.Path)
        );
    }

    public static RemoveDuplicateSongsInPlaylistResultVm RemoveDuplicateSongs(string playlistPath)
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

    public static ComparePlaylistsResultVm Compare(string playlist1Path, string playlist2Path)
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

    public static SortPlaylistResultVm SortSongs(string playlistPath)
    {
        var playlist = Playlist.Parse(playlistPath: playlistPath);
        playlist.SortItems();
        playlist.Save();

        return new SortPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            SortedSongs: playlist.Items.Select(i => i.Path)
        );
    }

    public static UpdateSongsPathResultVm UpdateSongsBasePath(string playlistPath, string oldBasePath, string newBasePath, bool isNewBasePathLinuxBased, string destinationDirectoryPath)
    {
        var playlist = Playlist.Parse(playlistPath: playlistPath);
        playlist.UpdateItemsBasePath(
            currentBasePath: oldBasePath,
            newBasePath: newBasePath,
            isNewBasePathLinuxBased: isNewBasePathLinuxBased
        );
        playlist.Save(destinationDirectoryPath: destinationDirectoryPath);

        return new UpdateSongsPathResultVm(
            PlaylistTitle: playlist.Title
        );
    }
}
