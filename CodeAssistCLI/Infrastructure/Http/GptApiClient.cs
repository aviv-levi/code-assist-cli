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
//
// public static class GptApiClient
// {
//     public static async Task<string> SendToGptAsync(string prompt)
//     {
//         string ApiUrl = "https://api.openai.com/v1/chat/completions";
//         string ApiKey = "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"; // Replace with your OpenAI API key.
//
//         using (HttpClient client = new HttpClient())
//         {
//             client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");
//
//             var requestBody = new
//             {
//                 model = "gpt-4o-mini",
//                 messages = new[]
//                 {
//                     new { role = "system", content = "You are a helpful assistant." },
//                     new { role = "user", content = prompt }
//                 }
//             };
//
//             string jsonContent = JsonConvert.SerializeObject(requestBody);
//             var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
//
//             HttpResponseMessage response = await client.PostAsync(ApiUrl, httpContent);
//             if (response.IsSuccessStatusCode)
//             {
//                 string responseContent = await response.Content.ReadAsStringAsync();
//                 var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
//                 return responseObject.choices[0].message.content.ToString();
//             }
//             else
//             {
//                 return $"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
//             }
//         }
//     }
// }