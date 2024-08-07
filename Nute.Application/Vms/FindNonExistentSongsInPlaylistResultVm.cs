namespace Nute.Application.Vms;

public record FindNonExistentSongsInPlaylistResultVm(string PlaylistTitle, IEnumerable<string> NonExistentSongs);
