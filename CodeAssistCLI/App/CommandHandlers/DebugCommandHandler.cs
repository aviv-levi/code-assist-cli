using CodeAssistCLI.App.Commands;

namespace CodeAssistCLI.App.CommandHandlers;

public static class DebugCommandHandler
{
    public static int HandleCommand(DebugCommand command)
    {
        Console.WriteLine("Debug Command");
        return 1;
    }
}