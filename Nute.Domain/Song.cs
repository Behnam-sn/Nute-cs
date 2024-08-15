namespace Nute.Domain;

public sealed class Song
{
    public string Path { get; }
    public string Title { get; }

    public Song()
    {

    }

    internal static Song Parse(string path)
    {
        throw new NotImplementedException();
    }
}
