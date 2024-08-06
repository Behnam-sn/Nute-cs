namespace Nute.Domain.Dtos;

public record RemoveDuplicateSongsInPlaylistResultDto(IEnumerable<string> Songs, IEnumerable<string> DuplicateSongs);
