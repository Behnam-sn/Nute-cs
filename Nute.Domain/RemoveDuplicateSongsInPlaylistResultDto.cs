namespace Nute.Domain;

public record RemoveDuplicateSongsInPlaylistResultDto(IEnumerable<string> Songs, IEnumerable<string> DuplicateSongs);
