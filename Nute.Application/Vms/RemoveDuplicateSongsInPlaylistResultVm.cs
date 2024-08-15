namespace Nute.Application.Vms;

public record RemoveDuplicateSongsInPlaylistResultVm(string PlaylistTitle, IEnumerable<string> DuplicateSongs);
