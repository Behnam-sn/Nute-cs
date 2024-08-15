namespace Nute.Domain.Dtos;

public sealed record ComparePlaylistsResultDto(
    IEnumerable<Song> Playlist1UniqueSongs,
    IEnumerable<Song> Playlist2UniqueSongs,
    IEnumerable<Song> InCommonSongs
);
