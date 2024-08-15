using System.Text;
using Nute.Domain.Dtos;

namespace Nute.Domain;

public sealed class Playlist
{
    private const string PLAYLIST_TYPE = ".m3u8";

    private List<Song> _songs = [];

    public string Path { get; }
    public string Title { get; }
    public IEnumerable<Song> Songs => _songs;
    public IEnumerable<string> NotFoundedSongs { get; }

    private Playlist(string path, string title, IEnumerable<Song> songs, IEnumerable<string> notFoundedSongs)
    {
        Path = path;
        Title = title;
        _songs = songs.ToList();
        NotFoundedSongs = notFoundedSongs;
    }

    public IEnumerable<Song> GetDuplicateSongs()
    {
        var uniqueSongs = new HashSet<string>();
        var duplicateSongs = new List<Song>();

        foreach (var song in Songs)
        {
            if (uniqueSongs.Add(song.Path) is false)
            {
                duplicateSongs.Add(song);
            }
        }

        return duplicateSongs;
    }

    public IEnumerable<Song> RemoveDuplicateSongs()
    {
        var duplicateSongs = GetDuplicateSongs();

        _songs = _songs.Where(i => duplicateSongs.Contains(i) is false).ToList();

        return duplicateSongs;
    }

    public ComparePlaylistsResultDto CompareTo(Playlist otherPlaylist)
    {
        var inCommonSongs = new List<Song>();

        foreach (var song in Songs)
        {
            if (otherPlaylist.Songs.Contains(song))
            {
                inCommonSongs.Add(song);
            }
        }

        var playlist1UniqueSongs = Songs.ToList();
        playlist1UniqueSongs.RemoveAll(i => inCommonSongs.Contains(i));

        var playlist2UniqueSongs = otherPlaylist.Songs.ToList();
        playlist2UniqueSongs.RemoveAll(i => inCommonSongs.Contains(i));

        return new ComparePlaylistsResultDto(
            Playlist1UniqueSongs: playlist1UniqueSongs,
            Playlist2UniqueSongs: playlist2UniqueSongs,
            InCommonSongs: inCommonSongs
        );
    }

    public void Save()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("#EXTM3U");
        stringBuilder.AppendLine($"#{Title}{PLAYLIST_TYPE}");
        foreach (var song in Songs)
        {
            stringBuilder.AppendLine(song.Path);
        }
        foreach (var song in NotFoundedSongs)
        {
            stringBuilder.AppendLine(song);
        }
        //stringBuilder.Append("");
        File.WriteAllText(Path, stringBuilder.ToString());
    }

    public static Playlist Parse(string path)
    {
        var fileExtension = System.IO.Path.GetExtension(path);
        if (fileExtension is not PLAYLIST_TYPE)
        {
            throw new PlaylistTypeNotAcceptableDomainException($"{fileExtension} is Not a Acceptable Playlist Type.");
        }

        var playlistLines = File.ReadAllLines(path);
        var title = ExtractPlaylistTitle(playlistLines);

        var songsPaths = playlistLines[2..];
        var songs = new List<Song>();
        var notFoundedSongs = new List<string>();

        foreach (var songPath in songsPaths)
        {
            try
            {
                var song = Song.Parse(path: songPath);
                songs.Add(song);
            }
            catch (SongFileNotExistDomainException)
            {
                notFoundedSongs.Add(songPath);
            }
            catch (SongFilePathIsInvalidDomainException)
            {
            }
        }

        return new Playlist(
            path: path,
            title: title,
            songs: songs,
            notFoundedSongs: notFoundedSongs
        );
    }

    private static string ExtractPlaylistTitle(string[] playlistLines)
    {
        var titleLine = playlistLines[1];
        var lastIndexOfTag = titleLine.LastIndexOf(PLAYLIST_TYPE);
        return titleLine[1..lastIndexOfTag];
    }
}
