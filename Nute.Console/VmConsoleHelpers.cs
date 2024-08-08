using Nute.Application.Vms;

namespace Nute.Console;

internal static class VmConsoleHelpers
{
    internal static void ShowInConsole(this ComparePlaylistsResultVm vm)
    {
        System.Console.WriteLine($"Playlist 1");
        System.Console.WriteLine($"Title: {vm.Playlist1Title}");
        System.Console.WriteLine("Unique Songs:");
        foreach (var song in vm.Playlist1Songs)
        {
            System.Console.WriteLine(song);
        }
        System.Console.WriteLine($"Playlist 2");
        System.Console.WriteLine($"Title: {vm.Playlist2Title}");
        System.Console.WriteLine("Unique Songs:");
        foreach (var song in vm.Playlist2Songs)
        {
            System.Console.WriteLine(song);
        }
        System.Console.WriteLine("In Common Songs: ");
        foreach (var song in vm.InCommonSongs)
        {
            System.Console.WriteLine(song);
        }
    }
}
