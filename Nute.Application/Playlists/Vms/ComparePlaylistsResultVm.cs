namespace Nute.Application.Playlists.Vms;

public record ComparePlaylistsResultVm(
    string Playlist1Title,
    IEnumerable<string> Playlist1Songs,
    string Playlist2Title,
    IEnumerable<string> Playlist2Songs,
    IEnumerable<string> InCommonSongs);
