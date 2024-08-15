namespace Nute.Domain;

public sealed class SongFileNotExistDomainException : Exception
{
    public SongFileNotExistDomainException(string? message) : base(message)
    {
    }
}
