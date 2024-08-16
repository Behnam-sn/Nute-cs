using Nute.Domain.Songs.Exceptions;

namespace Nute.Domain.Songs;

public sealed class Song : IComparable<Song>
{
    public string Path { get; }
    public string Artist { get; }
    public string Album { get; }
    public string Title { get; }
    public uint Track { get; }
    public uint Year { get; }
    public bool IsSingle { get; }

    public Song(string artist, string album, uint year, uint track)
    {
        //Path = path;
        Artist = artist;
        Album = album;
        //Title = title;
        Track = track;
        Year = year;

        if (Album.EndsWith("Single"))
        {
            IsSingle = true;
        }
    }

    public int CompareTo(Song? other)
    {
        if (other is null)
        {
            return 1;
        }

        if (Year < other.Year)
        {
            return -1;
        }

        if (Year > other.Year)
        {
            return 1;
        }

        if (!IsSingle && other.IsSingle)
        {
            return -1;
        }

        if (IsSingle && !other.IsSingle)
        {
            return 1;
        }

        if (IsSingle && other.IsSingle)
        {
            if (Artist != other.Artist)
            {
                return Artist.CompareTo(other.Artist);
            }

            if (Album != other.Album)
            {
                return Album.CompareTo(other.Album);
            }

            return 0;
        }

        if (!IsSingle && !other.IsSingle)
        {
            if (Artist != other.Artist)
            {
                return Artist.CompareTo(other.Artist);
            }

            if (Album != other.Album)
            {
                return Album.CompareTo(other.Album);
            }

            if (Track < other.Track)
            {
                return -1;
            }

            if (Track > other.Track)
            {
                return 1;
            }

            return 0;
        }

        return 0;
    }

    internal static Song Parse(string path)
    {
        if (path is null || path is "" || path is " ")
        {
            throw new SongFilePathIsInvalidDomainException($"{path} is Not a Valid Path.");
        }

        if (File.Exists(path) is false)
        {
            throw new SongFileNotExistDomainException($"{path} Doesn't Exist.");
        }

        var songFile = TagLib.File.Create(path: path);
        return new Song(
            artist: songFile.Tag.FirstAlbumArtist,
            album: songFile.Tag.Album,
            year: songFile.Tag.Year,
            track: songFile.Tag.Track
        );
    }
}
