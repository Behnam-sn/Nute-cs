namespace Nute.Domain;

public record FindNonExistentSongsInPlaylistResultDto(IEnumerable<string> NonExistentSongs);