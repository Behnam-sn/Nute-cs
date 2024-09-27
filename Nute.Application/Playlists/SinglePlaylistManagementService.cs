using Nute.Application.Playlists.Vms;
using Nute.Domain.Playlists;
using Nute.Domain.Playlists.Enums;

namespace Nute.Application.Playlists;

public static class SinglePlaylistManagementService
{
    public static GetNotFoundedSongsInPlaylistResultVm GetNotFoundedSongsInPlaylist(string playlistPath)
    {
        var playlist = Playlist.Parse(playlistPath: playlistPath);
        var notFoundedSongs = playlist.GetNotFoundedItems();

        return new GetNotFoundedSongsInPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            NotFoundedSongs: notFoundedSongs.Select(i => i.SongPath)
        );
    }

    public static GetDuplicateSongsInPlaylistResultVm GetDuplicateSongsInPlaylist(string playlistPath)
    {
        var playlist = Playlist.Parse(playlistPath: playlistPath);
        var duplicateSongs = playlist.GetDuplicateItems();

        return new GetDuplicateSongsInPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            DuplicateSongs: duplicateSongs.Select(i => i.SongPath)
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
            RemovedSongs: duplicateSongs.Select(i => i.SongPath)
        );
    }

    public static SortPlaylistResultVm SortSongsInPlaylist(string playlistPath)
    {
        var playlist = Playlist.Parse(playlistPath: playlistPath);
        playlist.SortItems();
        playlist.Save();

        return new SortPlaylistResultVm(
            PlaylistTitle: playlist.Title
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
}
