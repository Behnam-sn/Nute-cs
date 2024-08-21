using Nute.Domain.Playlists.Exceptions;

namespace Nute.Domain.Playlists;

public readonly struct PlaylistTitle
{
    public string Value { get; }

    private PlaylistTitle(string value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value;
    }

    internal static PlaylistTitle Parse(string playlistPath, string[] allPlaylistLines)
    {
        var fileName = Path.GetFileNameWithoutExtension(playlistPath);
        var titleLine = allPlaylistLines[1];

        if (!titleLine.Contains(fileName))
        {
            throw new PlaylistTitleNotFound($"Can't find playlist tittle in ${playlistPath}");
        }

        return new PlaylistTitle(
            value: fileName
        );
    }
}
