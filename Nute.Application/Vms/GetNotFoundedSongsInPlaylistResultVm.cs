namespace Nute.Application.Vms;

public record GetNotFoundedSongsInPlaylistResultVm(string PlaylistTitle, IEnumerable<string> NotFoundedSongs);
