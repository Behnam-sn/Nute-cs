namespace Nute.Domain.Dtos;

public record ComparePlaylistsResultDto(IEnumerable<string> Playlist1UniqueSongs, IEnumerable<string> Playlist2UniqueSongs, IEnumerable<string> InCommonSongs);
