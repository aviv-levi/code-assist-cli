using CodeAssistCLI.App.CommandHandlers;
using CodeAssistCLI.App.Commands;
using CommandLine;

namespace CodeAssistCLI.App.CLI;

public static class Application
{
    public static void Run(string[] args)
    {
        Parser.Default.ParseArguments<AnalyzeCommand, DebugCommand>(args).MapResult(
            (AnalyzeCommand opts) => AnalyzeCommandHandler.HandleCommand(opts),
            (DebugCommand opts) => DebugCommandHandler.HandleCommand(opts),
            errs => 1
        );
    }
}