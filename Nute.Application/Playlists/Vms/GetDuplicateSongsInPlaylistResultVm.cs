namespace Nute.Application.Playlists.Vms;

public sealed record GetDuplicateSongsInPlaylistResultVm(string PlaylistTitle, IEnumerable<string> DuplicateSongs);
