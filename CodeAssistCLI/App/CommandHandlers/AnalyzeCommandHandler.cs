using CodeAssistCLI.App.Commands;
using CodeAssistCLI.Core.Services;
using CodeAssistCLI.Infrastructure.IO;

namespace CodeAssistCLI.App.CommandHandlers;

public class AnalyzeCommandHandler
{
    private readonly CodeAnalysisService _codeAnalysisService;
    private readonly FileReader _fileReader;

    public AnalyzeCommandHandler(CodeAnalysisService codeAnalysisService, FileReader fileReader)
    {
        _codeAnalysisService = codeAnalysisService;
        _fileReader = fileReader;
    }

    public async Task<int> HandleCommand(AnalyzeCommand command)
    {
        try
        {
            Console.WriteLine($"Reading file: {command.File}");
            var codeContent = _fileReader.ReadFile(command.File);

            Console.WriteLine("Analyzing code...");
            var analysisResult = await _codeAnalysisService.AnalyzeCodeAsync(codeContent);

            Console.WriteLine("Analysis Results:");
            Console.WriteLine(analysisResult);
            return 0; // Success
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"[ERROR] {ex.Message}");
            return 1; // Failure
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] An unexpected error occurred: {ex.Message}");
            return 1; // Failure
        }
    }
}