namespace Nute.Domain;

public record PlaylistProcessorRemoveDuplicateSongsInPlaylistResult(IEnumerable<string> Songs, IEnumerable<string> DuplicateSongs);