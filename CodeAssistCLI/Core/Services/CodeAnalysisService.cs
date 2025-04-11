using CodeAssistCLI.Models;

namespace CodeAssistCLI.Core.Services
{
    public class CodeAnalysisService
    {
        private readonly GptService _gptService;

        public CodeAnalysisService(GptService gptService)
        {
            _gptService = gptService;
        }

        /// <summary>
        /// Analyzes C# code and returns actionable insights.
        /// </summary>
        /// <param name="code">The C# code to analyze.</param>
        /// <returns>A user-friendly analysis report.</returns>
        public async Task<string> AnalyzeCodeAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Code cannot be null or empty.", nameof(code));

            // Prepare the GPT request
            var request = new GptRequest
            {
                Prompt = GenerateAnalysisPrompt(code),
                MaxTokens = 1500,
                Temperature = 0.7
            };

            // Send the request via GptService
            var rawResponse = await _gptService.SendRequestAsync(request);

            // Process raw GPT response if needed (e.g., formatting, extracting key points)
            return ProcessAnalysisResponse(rawResponse);
        }

        /// <summary>
        /// Generates a prompt for GPT to analyze the given code.
        /// </summary>
        /// <param name="code">The code to analyze.</param>
        /// <returns>A formatted prompt string.</returns>
        private string GenerateAnalysisPrompt(string code)
        {
            return $@"
You are an AI assistant that performs code analysis and provides helpful insights. Analyze the following C# code and identify:

1. Any potential issues, including runtime errors, inefficiencies, or code smells.
2. Suggestions for improving code quality and maintainability.
3. Specific fixes or examples where applicable.

Respond in a structured format like this:
[INFO] Analysis Results:

[Line X] <Description of the issue>.
[Line Y] <Description of another issue>. ...
Suggested Fixes:

<Suggested fix for issue 1>.
<Suggested fix for issue 2>. ...

Here is the code to analyze:

```csharp
{code}
```
Answer without extra words only the template";
        }

        /// <summary>
        /// Processes raw GPT response and formats it for user output.
        /// </summary>
        /// <param name="response">Raw response from GPT.</param>
        /// <returns>Formatted analysis report.</returns>
        private string ProcessAnalysisResponse(string response)
        {
            if (string.IsNullOrWhiteSpace(response))
                return "[ERROR] Unable to process the request.";

            // Add additional processing if needed
            return $"\n[INFO] Analysis Results:\n\n{response}";
        }
    }
}
