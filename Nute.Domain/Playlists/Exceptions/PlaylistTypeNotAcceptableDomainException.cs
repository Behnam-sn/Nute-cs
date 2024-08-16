namespace Nute.Domain.Playlists.Exceptions;

public sealed class PlaylistTypeNotAcceptableDomainException : Exception
{
    public PlaylistTypeNotAcceptableDomainException(string? message) : base(message)
    {
    }
}
