namespace CodeAssistCLI.Models;

public class GptResponse
{
    public List<GptChoice> Choices { get; set; }
}

public class GptChoice
{
    public string Text { get; set; }
}