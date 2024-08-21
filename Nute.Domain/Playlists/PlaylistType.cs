using Nute.Domain.Playlists.Exceptions;

namespace Nute.Domain.Playlists;

public readonly struct PlaylistType
{
    public PlaylistTypes Value { get; }

    private PlaylistType(PlaylistTypes type)
    {
        Value = type;
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
                type: Enum.Parse<PlaylistTypes>(fileExtensionWithoutDot)
            );
        }
        catch (Exception)
        {
            throw new PlaylistTypeNotAcceptableDomainException($"{fileExtension} is Not a Acceptable Playlist Type.");
        }
    }
}
