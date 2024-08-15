namespace Nute.Application.Vms;

public sealed record RemoveDuplicateSongsInPlaylistResultVm(string PlaylistTitle, IEnumerable<string> DuplicateSongs);
