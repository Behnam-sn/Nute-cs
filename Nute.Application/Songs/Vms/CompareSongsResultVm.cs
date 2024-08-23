namespace Nute.Application.Songs.Vms;

public sealed record CompareSongsResultVm(IEnumerable<string> AddedSongs, IEnumerable<string> EditedSongs, IEnumerable<string> DeletedSongs);
