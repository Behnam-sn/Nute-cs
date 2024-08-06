namespace Nute.Domain.Dtos;

public record ComparePlaylistsResultDto(IEnumerable<string> Playlist1Songs, IEnumerable<string> Playlist2Songs, IEnumerable<string> InCommonSongs);
