namespace Nute.Console.CommandControllers;

internal sealed record Command(IEnumerable<string> Titles, Action Action);
