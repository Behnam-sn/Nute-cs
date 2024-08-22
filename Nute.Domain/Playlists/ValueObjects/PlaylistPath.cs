namespace Nute.Domain.Playlists.ValueObjects;

public readonly struct PlaylistPath
{
    public string Value { get; }

    private PlaylistPath(string value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value;
    }

    internal static PlaylistPath Parse(string playlistPath)
    {
        if (!File.Exists(playlistPath))
        {
            throw new FileNotFoundException($"{playlistPath} Not Founded.");
        }

        return new PlaylistPath(
            value: playlistPath
        );
    }

    public static implicit operator string(PlaylistPath playlistPath)
    {
        return playlistPath.ToString();
    }
}

