using CommandLine;

namespace CodeAssistCLI.App.Commands;

[Verb("debug", HelpText = "Add file contents to the index.")]
public class DebugCommand
{
    [Option('e', "error", Required = true, HelpText = "Set output to verbose messages.")]
    public string Error { get; set; }
}