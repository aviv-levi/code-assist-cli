using CodeAssistCLI.App.Commands;

namespace CodeAssistCLI.App.CommandHandlers;

public static class AnalyzeCommandHandler
{
    public static int HandleCommand(AnalyzeCommand command)
    {
        Console.WriteLine("Analyze command");
        return 1;
    }
}