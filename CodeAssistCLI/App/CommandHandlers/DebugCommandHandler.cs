using System.Text;
using CodeAssistCLI.App.Commands;
using CodeAssistCLI.Core.Services;

namespace CodeAssistCLI.App.CommandHandlers;

public class DebugCommandHandler
{
    private readonly string[] _notAllowedFiles = { ".exe", ".dll" , ".bin", "bin", "obj", ".idea", ".pdb", ".deps", ".runtimeconfig", ".csproj"};
    private readonly CodeDebuggingService _codeDebuggingService;

    public DebugCommandHandler(CodeDebuggingService codeDebuggingService)
    {
        _codeDebuggingService = codeDebuggingService;
    }
    
    public async Task<int> HandleCommand(DebugCommand command)
    {
        try
        {
            Console.WriteLine($"Reading local files..");
            var codeContent = ReadAllLocalFiles(command.Language);
            Console.WriteLine(codeContent);

            Console.WriteLine("Debugging code...");
            var debuggingResult = await _codeDebuggingService.DebugCodeAsync(codeContent);

            Console.WriteLine("Debugging Results:");
            Console.WriteLine(debuggingResult);
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

    private string ReadAllLocalFiles(string language)
    {
        string rootDirectory = Directory.GetCurrentDirectory();
        string[] allFiles = Directory.GetFiles(rootDirectory, "*.*", SearchOption.AllDirectories);

        StringBuilder result = new StringBuilder();

        foreach (string filePath in allFiles)
        {
            string relativePath = Path.GetRelativePath(rootDirectory, filePath);
            if (_notAllowedFiles.Any(f => relativePath.ToLower().Contains(f.ToLower()))) continue;
            
            string content = File.ReadAllText(filePath);
            result.AppendLine($"File Path: {relativePath}");
            result.AppendLine("File Content:");
            result.AppendLine($"```{language}\n{content}\n```");
            result.AppendLine(); // Separate entries with a blank line
        }

        return result.ToString();
    }
}