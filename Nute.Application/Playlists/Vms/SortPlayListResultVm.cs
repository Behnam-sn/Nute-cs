using Nute.Domain.Playlists;

namespace Nute.Application.Playlists.Vms;

public sealed record SortPlaylistResultVm(string PlaylistTitle, IEnumerable<string> SortedSongs);
