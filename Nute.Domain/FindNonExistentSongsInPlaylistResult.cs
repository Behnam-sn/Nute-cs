namespace Nute.Domain;

public record FindNonExistentSongsInPlaylistResult(IEnumerable<string> NonExistentSongs);