namespace Nute.Domain.Songs.Exceptions;

public sealed class SongFileNotExistDomainException : Exception
{
    public SongFileNotExistDomainException(string? message) : base(message)
    {
    }
}
