namespace Nute.Domain.Songs.Exceptions;

public sealed class SongFilePathIsInvalidDomainException : Exception
{
    public SongFilePathIsInvalidDomainException(string? message) : base(message)
    {
    }
}
