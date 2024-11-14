namespace Nute.Console.Contracts;

internal sealed record Command(IEnumerable<string> Titles, Action Action);
