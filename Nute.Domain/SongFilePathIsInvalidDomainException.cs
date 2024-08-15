namespace Nute.Domain;

public sealed class SongFilePathIsInvalidDomainException : Exception
{
    public SongFilePathIsInvalidDomainException(string? message) : base(message)
    {
    }
}
