namespace Nute.Application.Playlists.Vms;

public record GetNotFoundedSongsInPlaylistResultVm(string PlaylistTitle, IEnumerable<string> NotFoundedSongs);
