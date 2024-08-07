using Nute.Application.Vms;
using Nute.Domain;

namespace Nute.Application;

public class PlaylistsManagementService
{
    public static ComparePlaylistsResultVm ComparePlaylists(string playlistPath1, string playlistPath2)
    {
        ValidatePlaylist(playlistPath: playlistPath1);
        ValidatePlaylist(playlistPath: playlistPath2);

        var playlist1 = File.ReadAllLines(playlistPath1);
        var playlist2 = File.ReadAllLines(playlistPath2);
        var result = PlaylistProcessor.ComparePlaylists(
            playlist1: playlist1[3..],
            playlist2: playlist2[3..]);

        return new ComparePlaylistsResultVm(
            Playlist1Title: GetPlaylistTitle(playlist1),
            Playlist1Songs: result.Playlist1Songs,
            Playlist2Title: GetPlaylistTitle(playlist2),
            Playlist2Songs: result.Playlist2Songs,
            InCommonSongs: result.InCommonSongs);
    }

    public static RemoveDuplicateSongsInPlaylistResultVm RemoveDuplicateSongsInPlaylist(string playlistPath)
    {
        ValidatePlaylist(playlistPath: playlistPath);

        var playlist = File.ReadAllLines(playlistPath);
        var result = PlaylistProcessor.FindDuplicateSongsInPlaylist(playlist: playlist[3..]);
        var newPlaylist = playlist[..2].Concat(result.Songs);
        File.WriteAllLines(playlistPath, newPlaylist);

        return new RemoveDuplicateSongsInPlaylistResultVm(
            PlaylistTitle: GetPlaylistTitle(playlist),
            DuplicateSongs: result.DuplicateSongs);
    }

    public static FindNonExistentSongsInPlaylistResultVm FindNonExistentSongsInPlaylist(string playlistPath)
    {
        // open playlist file
        // turn song into a list
        // find non existent song
        // return them 
        // show them
        return new FindNonExistentSongsInPlaylistResultVm();
    }

    public static SortPlaylistResultVm SortPlaylist(string playlistPath)
    {
        return new SortPlaylistResultVm();
    }

    public static void AdaptPlaylistForAndroid(string playlistPath)
    {

    }

    private static void ValidatePlaylist(string playlistPath)
    {
        if (Path.GetExtension(playlistPath) is not "m3u8")
        {
            throw new Exception("Not a Acceptable Playlist Type");
        }
    }

    private static string GetPlaylistTitle(string[] playlist)
    {
        return playlist[2];
    }
}
