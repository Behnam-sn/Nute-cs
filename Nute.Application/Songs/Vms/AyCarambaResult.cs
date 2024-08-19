namespace Nute.Application.Songs.Vms;

internal sealed record AyCarambaResult(IEnumerable<string> AddedSongs, IEnumerable<string> UpdatedSongs);
