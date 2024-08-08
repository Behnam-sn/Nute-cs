using Nute.Application.Vms;
using Nute.Domain;

namespace Nute.Application;

public class PlaylistsManagementService
{
    public static RemoveDuplicateSongsInPlaylistResultVm RemoveDuplicateSongsInPlaylist(string playlistPath)
    {
        var playlist = new Playlist(path: playlistPath);
        var result = playlist.RemoveDuplicateSongs();
        playlist.Save();

        return new RemoveDuplicateSongsInPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            DuplicateSongs: result.DuplicateSongs);
    }

    public static FindNonExistentSongsInPlaylistResultVm FindNonExistentSongsInPlaylist(string playlistPath)
    {
        var playlist = new Playlist(path: playlistPath);
        var result = playlist.FindNonExistentSongs();

        return new FindNonExistentSongsInPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            NonExistentSongs: result.NonExistentSongs);
    }

    public static ComparePlaylistsResultVm ComparePlaylists(string playlistPath1, string playlistPath2)
    {
        var playlist1 = new Playlist(path: playlistPath1);
        var playlist2 = new Playlist(path: playlistPath2);
        var result = playlist1.CompareTo(playlist2);

        return new ComparePlaylistsResultVm(
            Playlist1Title: playlist1.Title,
            Playlist1Songs: result.Playlist1Songs,
            Playlist2Title: playlist2.Title,
            Playlist2Songs: result.Playlist2Songs,
            InCommonSongs: result.InCommonSongs);
    }

    public static SortPlaylistResultVm SortPlaylist(string playlistPath)
    {
        return new SortPlaylistResultVm();
    }

    public static void AdaptPlaylistForAndroid(string playlistPath)
    {

    }
}
