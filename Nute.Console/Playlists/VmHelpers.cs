using Nute.Application.Playlists.Vms;

namespace Nute.Console.Playlists;

internal static class VmHelpers
{
    internal static void PrintInConsole(this GetNotFoundedSongsInPlaylistResultVm vm)
    {
        System.Console.WriteLine($"Title: {vm.PlaylistTitle}");
        System.Console.WriteLine("Not Founded Songs:");
        foreach (var song in vm.NotFoundedSongs)
        {
            System.Console.WriteLine(song);
        }
        System.Console.WriteLine("");
    }

    internal static void PrintInConsole(this GetDuplicateSongsInPlaylistResultVm vm)
    {
        System.Console.WriteLine($"Title: {vm.PlaylistTitle}");
        System.Console.WriteLine("Duplicate Songs:");
        foreach (var song in vm.DuplicateSongs)
        {
            System.Console.WriteLine(song);
        }
        System.Console.WriteLine("");
    }

    internal static void PrintInConsole(this RemoveDuplicateSongsInPlaylistResultVm vm)
    {
        System.Console.WriteLine($"Title: {vm.PlaylistTitle}");
        System.Console.WriteLine("Duplicate Songs:");
        foreach (var song in vm.DuplicateSongs)
        {
            System.Console.WriteLine(song);
        }
        System.Console.WriteLine("");
    }

    internal static void PrintInConsole(this ComparePlaylistsResultVm vm)
    {
        System.Console.WriteLine($"Playlist 1");
        System.Console.WriteLine($"Title: {vm.Playlist1Title}");
        System.Console.WriteLine("Unique Songs:");
        foreach (var song in vm.Playlist1Songs)
        {
            System.Console.WriteLine(song);
        }
        System.Console.WriteLine("");
        System.Console.WriteLine($"Playlist 2");
        System.Console.WriteLine($"Title: {vm.Playlist2Title}");
        System.Console.WriteLine("Unique Songs:");
        foreach (var song in vm.Playlist2Songs)
        {
            System.Console.WriteLine(song);
        }
        System.Console.WriteLine("");
        System.Console.WriteLine("In Common Songs: ");
        foreach (var song in vm.InCommonSongs)
        {
            System.Console.WriteLine(song);
        }
        System.Console.WriteLine("");
    }
}
