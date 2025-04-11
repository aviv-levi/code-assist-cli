using System.IO;

namespace CodeAssistCLI.Infrastructure.IO;

public class FileReader
{
    public string ReadFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file '{filePath}' was not found.");
        }

        return File.ReadAllText(filePath);
    }
}