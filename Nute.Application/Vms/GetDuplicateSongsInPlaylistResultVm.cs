namespace Nute.Application.Vms;

public sealed record GetDuplicateSongsInPlaylistResultVm(string PlaylistTitle, IEnumerable<string> DuplicateSongs);
