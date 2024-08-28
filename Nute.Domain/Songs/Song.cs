using Nute.Domain.Songs.Exceptions;

namespace Nute.Domain.Songs;

public sealed class Song : IEquatable<Song>, IComparable<Song>
{
    public string Path { get; }
    public string Artist { get; }
    public string Album { get; }
    public string Title { get; }
    public uint Track { get; }
    public uint Year { get; }

    public Song(string path, string artist, string album, string title, uint year, uint track)
    {
        Path = path;
        Artist = artist;
        Album = album;
        Title = title;
        Track = track;
        Year = year;
    }

    public bool Equals(Song? other)
    {
        throw new NotImplementedException();
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

        if (Title != other.Title)
        {
            return Title.CompareTo(other.Title);
        }

        return 0;
    }

    public static Song Parse(string path)
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
            path: path,
            artist: songFile.Tag.FirstAlbumArtist,
            album: songFile.Tag.Album,
            title: songFile.Tag.Title,
            year: songFile.Tag.Year,
            track: songFile.Tag.Track
        );
    }
}
