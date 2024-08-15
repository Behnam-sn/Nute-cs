namespace Nute.Domain;

public sealed class PlaylistTypeNotAcceptableDomainException : Exception
{
    public PlaylistTypeNotAcceptableDomainException(string? message) : base(message)
    {
    }
}
