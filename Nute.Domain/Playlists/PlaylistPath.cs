using Nute.Domain.Playlists.Exceptions;

namespace Nute.Domain.Playlists;

public struct PlaylistPath
{
    private const string PLAYLIST_TYPE = ".m3u8";

    public string Path { get; }
    public string Type { get; }

    private PlaylistPath(string path, string type)
    {
        Path = path;
        Type = type;
    }

    private static void ValidatePlaylistPath(string playlistPath)
    {
        var fileExtension = System.IO.Path.GetExtension(playlistPath);
        if (fileExtension is not PLAYLIST_TYPE)
        {
            throw new PlaylistTypeNotAcceptableDomainException($"{fileExtension} is Not a Acceptable Playlist Type.");
        }
    }

    internal static PlaylistPath Parse(string playlistPath)
    {
        ValidatePlaylistPath(playlistPath);

        return new PlaylistPath(
            path: playlistPath,
            type: PLAYLIST_TYPE
        );
    }
}

