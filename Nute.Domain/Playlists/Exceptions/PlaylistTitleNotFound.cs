namespace Nute.Domain.Playlists.Exceptions;

public sealed class PlaylistTitleNotFound : Exception
{
    public PlaylistTitleNotFound(string? message) : base(message)
    {
    }
}
