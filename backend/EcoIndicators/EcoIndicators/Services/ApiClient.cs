using EcoIndicators.Models;
using System.Text.Json;
using System.Text;

namespace EcoIndicators.Services
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _http;

        public ApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<ApiResponse> FetchData(string url, string query)
        {
            var content = new StringContent(query, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<ApiResponse>(json, options) ?? new ApiResponse();
        }
    }
}
