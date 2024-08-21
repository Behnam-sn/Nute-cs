using Nute.Domain.Playlists.Enums;
using Nute.Domain.Playlists.Exceptions;

namespace Nute.Domain.Playlists.ValueObjects;

public readonly struct PlaylistType
{
    public PlaylistTypes Value { get; }

    private PlaylistType(PlaylistTypes value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    internal static PlaylistType Parse(string playlistPath)
    {
        var fileExtension = Path.GetExtension(playlistPath);
        var fileExtensionWithoutDot = fileExtension[1..];

        try
        {
            return new PlaylistType(
                value: Enum.Parse<PlaylistTypes>(fileExtensionWithoutDot)
            );
        }
        catch (Exception)
        {
            throw new PlaylistTypeNotAcceptableDomainException($"{fileExtension} is Not a Acceptable Playlist Type.");
        }
    }

    public static implicit operator string(PlaylistType playlistType)
    {
        return playlistType.ToString();
    }
}
