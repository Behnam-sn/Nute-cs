namespace Nute.Domain.Dtos;

public record FindDuplicateSongsInPlaylistResultDto(IEnumerable<string> UniqueSongs, IEnumerable<string> DuplicateSongs);
