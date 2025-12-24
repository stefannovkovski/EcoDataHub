using System.Text.Json;
using System.Text;
using EcoIndicators.Models.PulseEcoModels;

namespace EcoIndicators.Services.PulseEco {
    public class PulseEcoService {
        private readonly HttpClient _httpClient;

        public PulseEcoService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<EcoPulseSensorDataDto[]> GetCityAverageDataAsync(
            string cityName, string valueType, DateTime from, DateTime to, string avgLevel = "day", string sensorId = "sensor_dev_60237_141") {
            string fromStr = Uri.EscapeDataString(from.ToString("yyyy-MM-ddTHH:mm:ss+01:00"));
            string toStr = Uri.EscapeDataString(to.ToString("yyyy-MM-ddTHH:mm:ss+01:00"));

            string url = $"https://{cityName}.pulse.eco/rest/avgData/{avgLevel}?sensorId={sensorId}&type={valueType}&from={fromStr}&to={toStr}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<EcoPulseSensorDataDto[]>(json);
        }
    }
}

