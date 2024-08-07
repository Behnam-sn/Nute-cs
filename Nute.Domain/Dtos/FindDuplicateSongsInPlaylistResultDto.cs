namespace Nute.Domain.Dtos;

public record FindDuplicateSongsInPlaylistResultDto(IEnumerable<string> Songs, IEnumerable<string> DuplicateSongs);
