namespace Nute.Application.Playlists.Vms;

public sealed record RemoveDuplicateSongsInPlaylistResultVm(string PlaylistTitle, IEnumerable<string> DuplicateSongs);
