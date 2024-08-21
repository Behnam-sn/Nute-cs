using Nute.Domain.Songs;
using Nute.Domain.Songs.Exceptions;

namespace Nute.Domain.Playlists.Entities;

public sealed class PlaylistItem : IEquatable<PlaylistItem>
{
    public string Path { get; private set; }
    public int Index { get; private set; }
    public Song? Song { get; }

    private PlaylistItem(string path, int index, Song? song)
    {
        Path = path;
        Index = index;
        Song = song;
    }

    internal void UpdatePath(string newPath)
    {
        Path = newPath;
    }

    internal void UpdateIndex(int newIndex)
    {
        Index = newIndex;
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

    internal static PlaylistItem Parse(string path, int index)
    {
        Song? song = null;
        try
        {
            song = Song.Parse(path: path);
        }
        catch (SongFileNotExistDomainException)
        {
        }
        catch (SongFilePathIsInvalidDomainException)
        {
        }

        return new PlaylistItem(
            path: path,
            index: index,
            song: song
        );
    }
}
