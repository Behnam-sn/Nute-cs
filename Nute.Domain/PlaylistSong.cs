using System.Diagnostics.CodeAnalysis;

namespace Nute.Domain;

public sealed class PlaylistSong : IEqualityComparer<PlaylistSong>
{
    public int Index { get; private set; }
    public string Path { get; }
    public Song? Song { get; }

    internal PlaylistSong(int index, string path, Song? song)
    {
        Index = index;
        Path = path;
        Song = song;
    }

    internal void UpdateIndex(int index)
    {
        Index = index;
    }

    public bool Equals(PlaylistSong? x, PlaylistSong? y)
    {
        if (x is null || y is null)
        {
            return false;
        }

        if (x.Path == y.Path)
        {
            return true;
        }

        return false;
    }

    public int GetHashCode([DisallowNull] PlaylistSong obj)
    {
        return obj.Path.GetHashCode();
    }
}
