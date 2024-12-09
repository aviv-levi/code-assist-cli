using CommandLine;

namespace CodeAssistCLI.App.Commands;

[Verb("analyze", HelpText = "Add file contents to the index.")]
public class AnalyzeCommand
{
    [Option('l', "language", Required = true, HelpText = "Set output to verbose messages.")]
    public string Language { get; set; }
    [Option('f', "file", Required = true, HelpText = "Set output to verbose messages.")]
    public string File { get; set; }
}