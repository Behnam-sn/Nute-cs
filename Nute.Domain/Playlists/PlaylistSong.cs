using Nute.Domain.Songs;

namespace Nute.Domain.Playlists;

public sealed class PlaylistSong : IEquatable<PlaylistSong>
{
    public string Path { get; }
    public int Index { get; private set; }
    public Song? Value { get; }

    internal PlaylistSong(string path, int index, Song? song)
    {
        Path = path;
        Index = index;
        Value = song;
    }

    internal void UpdateIndex(int newIndex)
    {
        Index = newIndex;
    }

    public bool Equals(PlaylistSong? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return other.Path == Path;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (obj is not PlaylistSong entity)
        {
            return false;
        }

        return entity.Path == Path;
    }

    public override int GetHashCode()
    {
        return Path.GetHashCode();
    }
}
