namespace Nute.Domain;

public record PlaylistProcessorComparePlaylistsResult(IEnumerable<string> Playlist1Songs, IEnumerable<string> Playlist2Songs, IEnumerable<string> InCommonSongs);