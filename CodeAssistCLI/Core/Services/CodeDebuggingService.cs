using CodeAssistCLI.Models;

namespace CodeAssistCLI.Core.Services;

public class CodeDebuggingService
{
    private readonly GptService _gptService;

        public CodeDebuggingService(GptService gptService)
        {
            _gptService = gptService;
        }

        /// <summary>
        /// Analyzes C# code and returns actionable insights.
        /// </summary>
        /// <param name="code">The C# code to analyze.</param>
        /// <returns>A user-friendly analysis report.</returns>
        public async Task<string> DebugCodeAsync(string localCode)
        {
            if (string.IsNullOrWhiteSpace(localCode))
                throw new ArgumentException("Code cannot be null or empty.", nameof(localCode));

            // Prepare the GPT request
            var request = new GptRequest
            {
                Prompt = GenerateDebuggingPrompt(localCode),
                MaxTokens = 1500,
                Temperature = 0.7
            };

            // Send the request via GptService
            var rawResponse = await _gptService.SendRequestAsync(request);

            // Process raw GPT response if needed (e.g., formatting, extracting key points)
            return ProcessDebuggingResponse(rawResponse);
        }

        /// <summary>
        /// Generates a prompt for GPT to analyze the given code.
        /// </summary>
        /// <param name="code">The code to analyze.</param>
        /// <returns>A formatted prompt string.</returns>
        private string GenerateDebuggingPrompt(string code)
        {
            return $@"
You are an AI assistant that specializes in debugging C# code. Analyze the following code snippet and identify:

Any bugs or incorrect logic that could cause unexpected behavior or errors.

Root causes of the bugs where possible.

Step-by-step suggestions for fixing the issues.

Respond in a structured format like this: [DEBUG] Bug Report:

[Line X] <Description of the bug>. [Line Y] <Description of another bug>. ...

Root Causes:

<Explanation of root cause for bug 1>. <Explanation of root cause for bug 2>. ...

Suggested Fixes:

<Step-by-step fix for bug 1>. <Step-by-step fix for bug 2>. ...

Here is the project code to debug:

{code}

Answer without extra words only the template";
        }

        /// <summary>
        /// Processes raw GPT response and formats it for user output.
        /// </summary>
        /// <param name="response">Raw response from GPT.</param>
        /// <returns>Formatted analysis report.</returns>
        private string ProcessDebuggingResponse(string response)
        {
            if (string.IsNullOrWhiteSpace(response))
                return "[ERROR] Unable to process the request.";

            // Add additional processing if needed
            return $"\n[INFO] Analysis Results:\n\n{response}";
        }
}