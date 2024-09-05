namespace Nute.Console.CommandControllers;

internal abstract class BaseCommandController
{
    protected abstract string Title { get; }
    protected List<Command> Commands { get; } = [];

    internal BaseCommandController()
    {
        Commands.AddRange([
            new(
                Titles: ["Help", "H"],
                Action: PrintCommands
            ),
            new(
                Titles: ["Quit", "Q"],
                Action: () => {}
            )
        ]);
    }

    private void PrintCommands()
    {
        foreach (var command in Commands)
        {
            System.Console.WriteLine(
                string.Join(
                    separator: ", ",
                    values: command.Titles
                )
            );
        }
        System.Console.WriteLine("");
    }

    internal void Run()
    {
        while (true)
        {
            System.Console.Write($"{Title} Command: ");
            var input = System.Console.ReadLine()?.ToLower();

            var command = Commands
                .FirstOrDefault(
                    i => i.Titles.Any(
                        j => j.Equals(input, StringComparison.CurrentCultureIgnoreCase)
                    )
                );

            if (command is null)
            {
                continue;
            }

            if (command.Titles.Contains("Quit"))
            {
                return;
            }

            try
            {
                command.Action();
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception.Message);
            }
        }
    }
}
