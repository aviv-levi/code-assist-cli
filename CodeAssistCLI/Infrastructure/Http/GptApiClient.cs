using System.Text;
using CodeAssistCLI.Models;
using System.Text.Json;


namespace CodeAssistCLI.Infrastructure.Http;

public class GptApiClient
{
    private readonly HttpClient _httpClient;

    public GptApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GptResponse> SendRequestAsync(GptRequest request)
    {
        var jsonRequest = JsonSerializer.Serialize(request);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://api.openai.com/v1/completions", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error communicating with GPT API: {response.StatusCode}");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<GptResponse>(jsonResponse);
    }
}