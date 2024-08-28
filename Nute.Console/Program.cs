using Nute.Console.CommandControllers;

namespace Nute.Console;

internal static class Program
{
    private static void Main(string[] args)
    {
        try
        {
            new NuteCommandController().Run();
        }
        catch (Exception exception)
        {
            System.Console.WriteLine(exception.Message);
        }
    }
}
