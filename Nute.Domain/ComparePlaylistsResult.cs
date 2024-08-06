namespace Nute.Domain;

public record ComparePlaylistsResult(IEnumerable<string> Playlist1Songs, IEnumerable<string> Playlist2Songs, IEnumerable<string> InCommonSongs);
