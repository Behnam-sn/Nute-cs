using System.Text;
using Nute.Domain.Playlists.Dtos;
using Nute.Domain.Playlists.Exceptions;

namespace Nute.Domain.Playlists;

public sealed class Playlist
{
    private const string PLAYLIST_TYPE = ".m3u8";

    public string Path { get; }
    public string Title { get; }
    public IEnumerable<PlaylistItem> Items { get; private set; }

    private Playlist(string path, string title, IEnumerable<PlaylistItem> items)
    {
        Path = path;
        Title = title;
        Items = items;
    }

    public IEnumerable<PlaylistItem> GetNotFoundedItems()
    {
        return Items.Where(i => i.Song is null);
    }

    public IEnumerable<PlaylistItem> GetDuplicateItems()
    {
        var uniqueItems = new HashSet<string>();
        var duplicateItems = new List<PlaylistItem>();

        foreach (var item in Items)
        {
            if (!uniqueItems.Add(item.Path))
            {
                duplicateItems.Add(item);
            }
        }

        return duplicateItems;
    }

    public void RemoveDuplicateItems()
    {
        var duplicateItems = GetDuplicateItems();
        Items = Items.Where(i => !duplicateItems.Contains(i));
    }

    public void SortItems()
    {
        Items = Items.OrderBy(i => i.Song);
        var index = 0;
        foreach (var item in Items)
        {
            item.UpdateIndex(newIndex: index);
            index++;
        }
    }

    public void UpdateItemsBasePath(string currentBasePath, string newBasePath, bool isNewBasePathLinuxBased)
    {
        foreach (var item in Items)
        {
            var newPath = item.Path.Replace(currentBasePath, newBasePath);
            if (isNewBasePathLinuxBased)
            {
                newPath = newPath.Replace("\\", "/");
            }
            item.UpdatePath(newPath: newPath);
        }
    }

    public void Save(string? destinationDirectoryPath = null)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine("#EXTM3U");
        stringBuilder.AppendLine($"#{Title}{PLAYLIST_TYPE}");

        var sortedItems = Items.OrderBy(i => i.Index);
        foreach (var item in sortedItems)
        {
            stringBuilder.AppendLine(item.Path);
        }

        var destinationPath = destinationDirectoryPath is null ? Path : System.IO.Path.Combine(destinationDirectoryPath, $"{Title}{PLAYLIST_TYPE}");
        File.WriteAllText(destinationPath, stringBuilder.ToString());
    }

    public ComparePlaylistsResultDto CompareTo(Playlist otherPlaylist)
    {
        var inCommonItems = Items.Where(i => otherPlaylist.Items.Contains(i));
        var playlist1UniqueItems = Items.Where(i => !inCommonItems.Contains(i));
        var playlist2UniqueItems = otherPlaylist.Items.Where(i => !inCommonItems.Contains(i));

        return new ComparePlaylistsResultDto(
            Playlist1UniqueItems: playlist1UniqueItems,
            Playlist2UniqueItems: playlist2UniqueItems,
            InCommonItems: inCommonItems
        );
    }

    public static Playlist Parse(string playlistPath)
    {
        ValidatePlaylistPath(playlistPath);

        var playlistLines = File.ReadAllLines(playlistPath);
        var title = ExtractPlaylistTitle(playlistLines);

        var itemsPaths = playlistLines[2..];
        var items = new List<PlaylistItem>();

        for (var i = 0; i < itemsPaths.Length; i++)
        {
            var item = PlaylistItem.Parse(
                path: itemsPaths[i],
                index: i
            );
            items.Add(item);
        }

        return new Playlist(
            path: playlistPath,
            title: title,
            items: items
        );
    }

    private static void ValidatePlaylistPath(string playlistPath)
    {
        var fileExtension = System.IO.Path.GetExtension(playlistPath);
        if (fileExtension is not PLAYLIST_TYPE)
        {
            throw new PlaylistTypeNotAcceptableDomainException($"{fileExtension} is Not a Acceptable Playlist Type.");
        }
    }

    private static string ExtractPlaylistTitle(string[] playlistLines)
    {
        var titleLine = playlistLines[1];
        var lastIndexOfTag = titleLine.LastIndexOf(PLAYLIST_TYPE);
        // TODO: Throw exception here
        return titleLine[1..lastIndexOfTag];
    }
}
