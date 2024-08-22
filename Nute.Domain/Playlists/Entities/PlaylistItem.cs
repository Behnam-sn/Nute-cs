using Nute.Domain.Songs;
using Nute.Domain.Songs.Exceptions;

namespace Nute.Domain.Playlists.Entities;

public sealed class PlaylistItem : IEquatable<PlaylistItem>
{
    private Lazy<Song?> _song;

    public string Path { get; private set; }
    public int Index { get; private set; }
    public Song? Song => _song.Value;

    private PlaylistItem(string path, int index)
    {
        Path = path;
        Index = index;
        _song = InitializeSong();
    }

    private Lazy<Song?> InitializeSong()
    {
        return new Lazy<Song?>(
            () =>
            {
                try
                {
                    return Song.Parse(path: Path);
                }
                catch (SongFileNotExistDomainException)
                {
                    return null;
                }
                catch (SongFilePathIsInvalidDomainException)
                {
                    return null;
                }
            }
       );
    }

    internal void UpdatePath(string newPath)
    {
        Path = newPath;
        _song = InitializeSong();
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
        return $"{Path}{Index}".GetHashCode();
    }

    internal static PlaylistItem Parse(string path, int index)
    {
        return new PlaylistItem(
            path: path,
            index: index
        );
    }
}
