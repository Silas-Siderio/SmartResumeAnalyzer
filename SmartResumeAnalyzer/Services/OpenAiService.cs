using System.Net.Http;
using System.Text;
using System.Text.Json;
using SmartResumeAnalyzer.Models;

namespace SmartResumeAnalyzer.Services
{
    public class OpenAiService
    {
        private readonly string _apiKey = "SUA_CHAVE_OPENAI_AQUI";
        private readonly HttpClient _httpClient = new();

        public async Task<string> AnalyzeResumeAsync(string resumeText)
        {
            var prompt = $"Analise o currículo a seguir e forneça:\nResumo geral;\nPontos fortes;\nPontos fracos;\nSugestões para melhorar.\n\nCurrículo:\n{resumeText}";

            var requestData = new
            {
                model = "gpt-4",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonDoc = JsonDocument.Parse(responseString);
            var output = jsonDoc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
            return output;
        }
    }
}