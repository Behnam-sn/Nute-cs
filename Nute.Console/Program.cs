using Nute.Console.CommandControllers;

namespace Nute.Console;

internal static class Program
{
    private static void Main()
    {
        new NuteCommandController().Run();
    }
}
