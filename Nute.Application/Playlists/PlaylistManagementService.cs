using Nute.Application.Playlists.Vms;
using Nute.Domain.Playlists;

namespace Nute.Application.Playlists;

public static class PlaylistManagementService
{
    public static IEnumerable<GetNotFoundedSongsInPlaylistResultVm> GetAllNotFoundedSongs(string sourcePath)
    {
        if (!Directory.Exists(sourcePath))
        {
            throw new Exception($"{sourcePath} doesn't exists.");
        }

        var playlists = Directory.EnumerateFiles(sourcePath);
        return playlists.Select(GetNotFoundedSongs);
    }

    public static IEnumerable<GetDuplicateSongsInPlaylistResultVm> GetAllDuplicateSongs(string sourcePath)
    {
        if (!Directory.Exists(sourcePath))
        {
            throw new Exception($"{sourcePath} doesn't exists.");
        }

        var playlists = Directory.EnumerateFiles(sourcePath);
        return playlists.Select(GetDuplicateSongs);
    }

    public static GetNotFoundedSongsInPlaylistResultVm GetNotFoundedSongs(string playlistPath)
    {
        var playlist = Playlist.Parse(playlistPath: playlistPath);
        var notFoundedSongs = playlist.GetNotFoundedSongs();

        return new GetNotFoundedSongsInPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            NotFoundedSongs: notFoundedSongs.Select(i => i.Path)
        );
    }

    public static GetDuplicateSongsInPlaylistResultVm GetDuplicateSongs(string playlistPath)
    {
        var playlist = Playlist.Parse(playlistPath: playlistPath);
        var duplicateSongs = playlist.GetDuplicateSongs();

        return new GetDuplicateSongsInPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            DuplicateSongs: duplicateSongs.Select(i => i.Path)
        );
    }

    public static RemoveDuplicateSongsInPlaylistResultVm RemoveDuplicateSongs(string playlistPath)
    {
        var playlist = Playlist.Parse(playlistPath: playlistPath);
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
        var playlist1 = Playlist.Parse(playlistPath: playlist1Path);
        var playlist2 = Playlist.Parse(playlistPath: playlist2Path);
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
        var playlist = Playlist.Parse(playlistPath: playlistPath);
        playlist.Sort();
        playlist.Save();

        return new SortPlaylistResultVm(
            PlaylistTitle: playlist.Title,
            SortedSongs: playlist.Songs.Select(i => i.Path)
        );
    }

    public static UpdateSongsPathResultVm UpdateSongsBasePath(string playlistPath, string oldBasePath, string newBasePath, bool isNewBasePathLinuxBased, string destinationDirectoryPath)
    {
        var playlist = Playlist.Parse(playlistPath: playlistPath);
        playlist.UpdateSongsBasePath(
            oldBasePath: oldBasePath,
            newBasePath: newBasePath,
            isNewBasePathLinuxBased: isNewBasePathLinuxBased
        );
        playlist.Save(destinationDirectoryPath: destinationDirectoryPath);

        return new UpdateSongsPathResultVm(
            PlaylistTitle: playlist.Title
        );
    }
}
