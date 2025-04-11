using System.Text;
using System.Text.Json;
using CodeAssistCLI.Models;

namespace CodeAssistCLI.Core.Services
{
    public class GptService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GptService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        /// <summary>
        /// Sends a raw request to the GPT API.
        /// </summary>
        /// <param name="request">The GPT request.</param>
        /// <returns>The raw response from GPT.</returns>
        public async Task<string> SendRequestAsync(GptRequest request)
        {
            var jsonRequest = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/completions")
            {
                Content = httpContent,
                Headers =
                {
                    { "Authorization", $"Bearer {_apiKey}" }
                }
            };

            using var response = await _httpClient.SendAsync(httpRequest);

            if (!response.IsSuccessStatusCode)
                return $"[ERROR] API request failed with status code {response.StatusCode}.";

            var responseContent = await response.Content.ReadAsStringAsync();
            var gptResponse = JsonSerializer.Deserialize<GptResponse>(responseContent);

            return gptResponse?.Choices?[0]?.Text?.Trim() ?? "[ERROR] No valid response received.";
        }
    }
}