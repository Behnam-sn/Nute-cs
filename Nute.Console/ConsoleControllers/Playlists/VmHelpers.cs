using Nute.Application.Playlists.Vms;

namespace Nute.Console.ConsoleControllers.Playlists;

internal static class VmHelpers
{
    internal static void PrintInConsole(this GetNotFoundedSongsInPlaylistResultVm vm)
    {
        PrintPlaylist(
            title: vm.PlaylistTitle,
            songs: vm.NotFoundedSongs
        );
    }

    internal static void PrintInConsole(this IEnumerable<GetNotFoundedSongsInPlaylistResultVm> vms)
    {
        foreach (var vm in vms)
        {
            vm.PrintInConsole();
        }
    }

    internal static void PrintInConsole(this GetDuplicateSongsInPlaylistResultVm vm)
    {
        PrintPlaylist(
            title: vm.PlaylistTitle,
            songs: vm.DuplicateSongs
        );
    }

    internal static void PrintInConsole(this IEnumerable<GetDuplicateSongsInPlaylistResultVm> vms)
    {
        foreach (var vm in vms)
        {
            vm.PrintInConsole();
        }
    }

    internal static void PrintInConsole(this RemoveDuplicateSongsInPlaylistResultVm vm)
    {
        PrintPlaylist(
            title: vm.PlaylistTitle,
            songs: vm.RemovedSongs
        );
    }

    internal static void PrintInConsole(this IEnumerable<RemoveDuplicateSongsInPlaylistResultVm> vms)
    {
        foreach (var vm in vms)
        {
            vm.PrintInConsole();
        }
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

    internal static void PrintInConsole(this IEnumerable<ComparePlaylistsResultVm> vms)
    {
        foreach (var vm in vms)
        {
            vm.PrintInConsole();
        }
    }

    internal static void PrintInConsole(this SortPlaylistResultVm vm)
    {
        System.Console.WriteLine(vm.PlaylistTitle);
    }

    internal static void PrintInConsole(this IEnumerable<SortPlaylistResultVm> vms)
    {
        foreach (var vm in vms)
        {
            vm.PrintInConsole();
        }
    }

    internal static void PrintInConsole(this UpdateSongsPathResultVm vm)
    {
        System.Console.WriteLine(vm.PlaylistTitle);
    }

    internal static void PrintInConsole(this IEnumerable<UpdateSongsPathResultVm> vms)
    {
        foreach (var vm in vms)
        {
            vm.PrintInConsole();
        }
    }

    private static void PrintPlaylist(string title, IEnumerable<string> songs)
    {
        if (songs.Any())
        {
            System.Console.WriteLine($"{title}:");
            foreach (var song in songs)
            {
                System.Console.WriteLine(song);
            }
            System.Console.WriteLine("");
        }
    }
}
