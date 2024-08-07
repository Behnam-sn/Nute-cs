namespace Nute.Domain.Dtos;

public record RemoveDuplicateSongsInPlaylistResultDto(IEnumerable<string> UniqueSongs, IEnumerable<string> DuplicateSongs);
