using Nute.Application.Vms;

namespace Nute.Console;

internal static class VmHelpers
{
    internal static void ShowInConsole(this ComparePlaylistsResultVm vm)
    {
        System.Console.WriteLine($"Playlist 1 Title: {vm.Playlist1Title}");
        System.Console.WriteLine("Playlist 1 Unique Songs:");
        foreach (var song in vm.Playlist1Songs)
        {
            System.Console.WriteLine(song);
        }
        System.Console.WriteLine("");
        System.Console.WriteLine($"Playlist 2 Title: {vm.Playlist2Title}");
        System.Console.WriteLine("Playlist 2 Unique Songs:");
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
    }
}
