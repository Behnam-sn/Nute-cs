using Nute.Domain.Songs;
using Nute.Domain.Songs.Exceptions;

namespace Nute.Domain.Playlists.Entities;

public sealed class PlaylistItem : IEquatable<PlaylistItem>
{
    private Lazy<Song?> _song;

    public int Index { get; private set; }
    public string SongPath { get; private set; }
    public Song? Song => _song.Value;

    private PlaylistItem(int index, string songPath)
    {
        Index = index;
        SongPath = songPath;
        _song = InitializeSong();
    }

    private Lazy<Song?> InitializeSong()
    {
        return new Lazy<Song?>(
            () =>
            {
                try
                {
                    return Song.Parse(path: SongPath);
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

    internal void UpdateIndex(int newIndex)
    {
        Index = newIndex;
    }

    internal void UpdateSongPath(string newSongPath)
    {
        SongPath = newSongPath;
        _song = InitializeSong();
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

        return other.SongPath == SongPath;
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

        return entity.SongPath == SongPath;
    }

    public override int GetHashCode()
    {
        return $"{Index}{SongPath}".GetHashCode();
    }

    internal static PlaylistItem Parse(int index, string songPath)
    {
        return new PlaylistItem(
            index: index,
            songPath: songPath
        );
    }
}
