using Nute.Domain.Songs;

namespace Nute.Domain.Playlists;

public sealed class PlaylistItem : IEquatable<PlaylistItem>
{
    public string Path { get; private set; }
    public int Index { get; private set; }
    public Song? Value { get; }

    internal PlaylistItem(string path, int index, Song? song)
    {
        Path = path;
        Index = index;
        Value = song;
    }

    internal void UpdateIndex(int newIndex)
    {
        Index = newIndex;
    }

    internal void UpdatePath(string newPath)
    {
        Path = newPath;
    }

    public bool Equals(PlaylistItem? other)
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

        if (obj is not PlaylistItem entity)
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
