namespace Nute.Console;

internal static class Program
{
    private static void Main(string[] args)
    {
        try
        {
            NuteCommandController.Run();
        }
        catch (Exception exception)
        {
            System.Console.WriteLine(exception.Message);
        }
    }
}
