namespace CodeAssistCLI.Models;

public class GptRequest
{
    public string Prompt { get; set; }
    public int MaxTokens { get; set; } = 1500;
    public double Temperature { get; set; } = 0.7;
}