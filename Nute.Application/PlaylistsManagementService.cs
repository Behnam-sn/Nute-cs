using Nute.Application.Vms;
using Nute.Domain;

namespace Nute.Application;

public static class PlaylistsManagementService
{
    public static GetNotFoundedSongsInPlaylistResultVm GetNotFoundedSongsInPlaylist(string playlistPath)
    {
        var playlist = Playlist.Parse(path: playlistPath);

        return new GetNotFoundedSongsInPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            NotFoundedSongs: playlist.NotFoundedSongs
        );
    }

    public static GetDuplicateSongsInPlaylistResultVm GetDuplicateSongsInPlaylist(string playlistPath)
    {
        var playlist = Playlist.Parse(path: playlistPath);

        return new GetDuplicateSongsInPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            DuplicateSongs: playlist.GetDuplicateSongs()
        );
    }

    public static RemoveDuplicateSongsInPlaylistResultVm RemoveDuplicateSongsInPlaylist(string playlistPath)
    {
        var playlist = Playlist.Parse(path: playlistPath);
        var duplicateSongs = playlist.RemoveDuplicateSongs();
        playlist.Save();

        return new RemoveDuplicateSongsInPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            DuplicateSongs: duplicateSongs
        );
    }

    public static ComparePlaylistsResultVm ComparePlaylists(string playlist1Path, string playlist2Path)
    {
        var playlist1 = Playlist.Parse(path: playlist1Path);
        var playlist2 = Playlist.Parse(path: playlist2Path);
        var result = playlist1.CompareTo(playlist2);

        return new ComparePlaylistsResultVm(
            Playlist1Title: playlist1.Title,
            Playlist1Songs: result.Playlist1UniqueSongs,
            Playlist2Title: playlist2.Title,
            Playlist2Songs: result.Playlist2UniqueSongs,
            InCommonSongs: result.InCommonSongs
        );
    }

    public static SortPlaylistResultVm SortPlaylist(string playlistPath)
    {
        return new SortPlaylistResultVm();
    }

    public static void AdaptPlaylistForAndroid(string playlistPath)
    {
    }
}
