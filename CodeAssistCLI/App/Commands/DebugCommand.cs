using CommandLine;

namespace CodeAssistCLI.App.Commands;

[Verb("debug", HelpText = "Add file contents to the index.")]
public class DebugCommand
{
    [Option('l', "language", Required = true, HelpText = "Set output to verbose messages.")]
    public string Language { get; set; }
}