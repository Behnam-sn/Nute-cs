using Nute.Console.CommandControllers;

namespace Nute.Console;

internal static class Program
{
    private static void Main(string[] args)
    {
        new NuteCommandController().Run();
    }
}
