namespace Nute.Domain.Playlists.ValueObjects;

public readonly struct PlaylistPath
{
    public string Value { get; }

    private PlaylistPath(string path)
    {
        Value = path;
    }

    public override string ToString()
    {
        return Value;
    }

    internal static PlaylistPath Parse(string playlistPath)
    {
        return new PlaylistPath(
            path: playlistPath
        );
    }

    public static implicit operator string(PlaylistPath playlistPath)
    {
        return playlistPath.ToString();
    }
}

