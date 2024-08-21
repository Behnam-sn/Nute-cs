namespace Nute.Domain.Playlists.Dtos;

public sealed record ComparePlaylistsResultDto(
    IEnumerable<PlaylistItem> Playlist1UniqueSongs,
    IEnumerable<PlaylistItem> Playlist2UniqueSongs,
    IEnumerable<PlaylistItem> InCommonSongs
);
