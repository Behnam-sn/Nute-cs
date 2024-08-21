namespace Nute.Domain.Playlists.Dtos;

public sealed record ComparePlaylistsResultDto(
    IEnumerable<PlaylistItem> Playlist1UniqueItems,
    IEnumerable<PlaylistItem> Playlist2UniqueItems,
    IEnumerable<PlaylistItem> InCommonItems
);
