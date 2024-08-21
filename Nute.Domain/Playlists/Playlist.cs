using System.Text;
using Nute.Domain.Playlists.Dtos;

namespace Nute.Domain.Playlists;

public sealed class Playlist
{
    public PlaylistPath Path { get; }
    public PlaylistType Type { get; }
    public string Title { get; }
    public IEnumerable<PlaylistItem> Items { get; private set; }

    public Playlist(PlaylistPath path, PlaylistType type, string title, IEnumerable<PlaylistItem> items)
    {
        Path = path;
        Type = type;
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
        stringBuilder.AppendLine($"#{Title}{Path.Type}");

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
        var type = PlaylistType.Parse();
        var path = PlaylistPath.Parse(
            playlistPath: playlistPath
        );

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
            path: path,
            type: type,
            title: title,
            items: items
        );
    }



    private static string ExtractPlaylistTitle(string[] playlistLines)
    {
        var titleLine = playlistLines[1];
        var lastIndexOfTag = titleLine.LastIndexOf(PLAYLIST_TYPE);
        // TODO: Throw exception here
        return titleLine[1..lastIndexOfTag];
    }
}
