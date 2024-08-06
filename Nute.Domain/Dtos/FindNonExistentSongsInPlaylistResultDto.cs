namespace Nute.Domain.Dtos;

public record FindNonExistentSongsInPlaylistResultDto(IEnumerable<string> NonExistentSongs);