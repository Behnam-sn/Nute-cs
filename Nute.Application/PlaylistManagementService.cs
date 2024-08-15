using Nute.Application.Vms;
using Nute.Domain;

namespace Nute.Application;

public static class PlaylistManagementService
{
    public static GetNotFoundedSongsInPlaylistResultVm GetNotFoundedSongs(string playlistPath)
    {
        var playlist = Playlist.Parse(path: playlistPath);
        var notFoundedSongs = playlist.GetNotFoundedSongs();

        return new GetNotFoundedSongsInPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            NotFoundedSongs: notFoundedSongs.Select(i => i.Path)
        );
    }

    public static GetDuplicateSongsInPlaylistResultVm GetDuplicateSongs(string playlistPath)
    {
        var playlist = Playlist.Parse(path: playlistPath);
        var duplicateSongs = playlist.GetDuplicateSongs();

        return new GetDuplicateSongsInPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            DuplicateSongs: duplicateSongs.Select(i => i.Path)
        );
    }

    public static RemoveDuplicateSongsInPlaylistResultVm RemoveDuplicateSongs(string playlistPath)
    {
        var playlist = Playlist.Parse(path: playlistPath);
        var duplicateSongs = playlist.GetDuplicateSongs();
        playlist.RemoveDuplicateSongs();
        playlist.Save();

        return new RemoveDuplicateSongsInPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            DuplicateSongs: duplicateSongs.Select(i => i.Path)
        );
    }

    public static ComparePlaylistsResultVm Compare(string playlist1Path, string playlist2Path)
    {
        var playlist1 = Playlist.Parse(path: playlist1Path);
        var playlist2 = Playlist.Parse(path: playlist2Path);
        var result = playlist1.CompareTo(playlist2);

        return new ComparePlaylistsResultVm(
            Playlist1Title: playlist1.Title,
            Playlist1Songs: result.Playlist1UniqueSongs.Select(i => i.Path),
            Playlist2Title: playlist2.Title,
            Playlist2Songs: result.Playlist2UniqueSongs.Select(i => i.Path),
            InCommonSongs: result.InCommonSongs.Select(i => i.Path)
        );
    }

    public static SortPlaylistResultVm Sort(string playlistPath)
    {
        return new SortPlaylistResultVm();
    }

    public static void AdaptForAndroid(string playlistPath)
    {
    }
}
