using Nute.Domain.Songs.Exceptions;

namespace Nute.Domain.Songs;

public sealed class Song
{
    public string Path { get; }
    public string Artist { get; }
    public string Album { get; }
    public string Title { get; }
    public uint? Index { get; }
    public uint Year { get; }
    public bool IsSingle { get; }

    public Song(string path, string artist, string album, string title, uint? index, uint year)
    {
        Path = path;
        Artist = artist;
        Album = album;
        Title = title;
        Index = index;
        Year = year;

        if (title.EndsWith("Single"))
        {
            IsSingle = true;
        }
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
            path: path,
            artist: songFile.Tag.FirstAlbumArtist,
            album: songFile.Tag.Album,
            title: songFile.Tag.Title,
            index: songFile.Tag.Track,
            year: songFile.Tag.Year
        );
    }
}
