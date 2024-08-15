namespace Nute.Domain;

public sealed class Song
{
    public string Path { get; }
    public string Title { get; }

    public Song(string path, string title)
    {
        Path = path;
        Title = title;
    }

    internal static Song Parse(string path)
    {
        var songFile = TagLib.File.Create(path: path);
        return new Song(
            path: path,
            title: songFile.Tag.Title
        );
    }
}
