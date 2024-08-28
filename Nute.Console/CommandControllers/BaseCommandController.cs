namespace Nute.Console.CommandControllers;

internal abstract class BaseCommandController
{
    protected readonly List<Command> _commands = [];

    internal BaseCommandController()
    {
        _commands.AddRange([
            new(
                Commands: ["Help", "H"],
                Action: PrintCommands
            ),
            new(
                Commands: ["Quit", "Q"],
                Action: () => {}
            )
        ]);
    }

    private void PrintCommands()
    {
        foreach (var command in _commands)
        {
            foreach (var item in command.Commands)
            {
                System.Console.Write(item);
                System.Console.Write(", ");
            }
            System.Console.WriteLine("");
        }
        System.Console.WriteLine("");
    }

    internal void Run()
    {
        while (true)
        {
            System.Console.Write("Command: ");
            var input = System.Console.ReadLine()?.ToLower();

            var command = _commands
                .FirstOrDefault(
                    i => i.Commands.Any(
                        j => j.Equals(input, StringComparison.CurrentCultureIgnoreCase)
                    )
                );

            if (command is null)
            {
                continue;
            }

            if (command.Commands.Contains("Quit"))
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
