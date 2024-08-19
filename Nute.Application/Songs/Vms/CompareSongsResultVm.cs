namespace Nute.Application.Songs.Vms;

public sealed record CompareSongsResultVm(IEnumerable<string> AddedSongs, IEnumerable<string> UpdatedSongs, IEnumerable<string> DeletedSongs);
