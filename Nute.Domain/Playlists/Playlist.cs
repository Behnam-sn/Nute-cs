using System.Text;
using Nute.Domain.Playlists.Dtos;
using Nute.Domain.Playlists.Entities;
using Nute.Domain.Playlists.Enums;
using Nute.Domain.Playlists.ValueObjects;

namespace Nute.Domain.Playlists;

public sealed class Playlist
{
    public PlaylistPath Path { get; }
    public PlaylistType Type { get; }
    public PlaylistTitle Title { get; }
    public IEnumerable<PlaylistItem> Items { get; private set; }

    private Playlist(PlaylistPath path, PlaylistType type, PlaylistTitle title, IEnumerable<PlaylistItem> items)
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
            if (!uniqueItems.Add(item.SongPath))
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

    public void UpdateItemsBasePath(string currentBasePath, PathTypes currentBasePathType, string newBasePath, PathTypes newBasePathType)
    {
        foreach (var item in Items)
        {
            var newSongPath = item.SongPath.Replace(currentBasePath, newBasePath);

            if ((currentBasePathType is PathTypes.Windows) && (newBasePathType is PathTypes.Linux))
            {
                newSongPath = newSongPath.Replace("\\", "/");
            }
            else if ((currentBasePathType is PathTypes.Linux) && (newBasePathType is PathTypes.Windows))
            {
                newSongPath = newSongPath.Replace("/", "\\");
            }

            item.UpdateSongPath(newSongPath: newSongPath);
        }
    }

    public void Save(string? destinationDirectoryPath = null)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine("#EXTM3U");
        stringBuilder.AppendLine($"#{Title}.{Type}");

        var sortedItems = Items.OrderBy(i => i.Index);
        foreach (var item in sortedItems)
        {
            stringBuilder.AppendLine(item.SongPath);
        }

        var destinationPath = destinationDirectoryPath is null ? Path : System.IO.Path.Combine(destinationDirectoryPath, $"{Title}.{Type}");
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
        var allPlaylistLines = File.ReadAllLines(playlistPath);

        var path = PlaylistPath.Parse(
            playlistPath: playlistPath
        );

        var type = PlaylistType.Parse(
            playlistPath: playlistPath
        );

        var title = PlaylistTitle.Parse(
            playlistPath: playlistPath,
            allPlaylistLines: allPlaylistLines
        );

        var itemsPaths = allPlaylistLines[2..];
        var items = new List<PlaylistItem>();

        for (var index = 0; index < itemsPaths.Length; index++)
        {
            var item = PlaylistItem.Parse(
                index: index,
                songPath: itemsPaths[index]
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
}
