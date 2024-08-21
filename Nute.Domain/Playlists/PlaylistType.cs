namespace Nute.Domain.Playlists;

public struct PlaylistType
{
    public PlaylistTypes Type { get; }

    private PlaylistType(PlaylistTypes type)
    {
        Type = type;
    }

    internal static PlaylistType Parse()
    {
        return new PlaylistType(
            type: PlaylistTypes.m3u8
        );
    }
}
