namespace Nute.Domain;

public record RemoveDuplicateSongsInPlaylistResult(IEnumerable<string> Songs, IEnumerable<string> DuplicateSongs);
