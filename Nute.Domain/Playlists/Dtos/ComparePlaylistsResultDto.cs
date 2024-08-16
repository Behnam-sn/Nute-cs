namespace Nute.Domain.Playlists.Dtos;

public sealed record ComparePlaylistsResultDto(
    IEnumerable<PlaylistSong> Playlist1UniqueSongs,
    IEnumerable<PlaylistSong> Playlist2UniqueSongs,
    IEnumerable<PlaylistSong> InCommonSongs
);
