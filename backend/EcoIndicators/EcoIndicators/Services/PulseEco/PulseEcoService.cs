using System.Text.Json;
using EcoIndicators.Models.PulseEcoModels;

namespace EcoIndicators.Services.PulseEco {
    public class PulseEcoService : IPulseEcoService {
        private readonly HttpClient _httpClient;

        private const string DefaultAvgLevel = "day";
        private const string DefaultSensorId = "-1";

        public PulseEcoService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<EcoPulseSensorDataDto[]> GetCityAverageDataAsync(
            string cityName,
            string valueType,
            DateTime from,
            DateTime to,
            string avgLevel,
            string sensorId) {
            avgLevel = string.IsNullOrWhiteSpace(avgLevel)
                ? DefaultAvgLevel
                : avgLevel;

            sensorId = string.IsNullOrWhiteSpace(sensorId)
                ? DefaultSensorId
                : sensorId;

            string fromStr = Uri.EscapeDataString(
                from.ToString("yyyy-MM-ddTHH:mm:ss+01:00")
            );

            string toStr = Uri.EscapeDataString(
                to.ToString("yyyy-MM-ddTHH:mm:ss+01:00")
            );

            string url =
                $"https://{cityName}.pulse.eco/rest/avgData/{avgLevel}" +
                $"?sensorId={sensorId}" +
                $"&type={valueType}" +
                $"&from={fromStr}" +
                $"&to={toStr}";

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<EcoPulseSensorDataDto[]>(
                json,
                new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true
                }
            ) ?? Array.Empty<EcoPulseSensorDataDto>();
        }
    }
}
