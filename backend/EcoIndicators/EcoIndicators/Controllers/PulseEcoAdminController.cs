using EcoIndicators.Services.PulseEco;
using Microsoft.AspNetCore.Mvc;

namespace EcoIndicators.Controllers {
    [ApiController]
    [Route("admin/pulseeco")]
    public class PulseEcoAdminController : ControllerBase {
        private readonly PulseEcoService _pulseService;

        public PulseEcoAdminController(PulseEcoService pulseService) {
            _pulseService = pulseService;
        }

        [HttpPost("populate")]
        public async Task<IActionResult> PopulatePulseEcoData(
            string city = "skopje",
            string metric = "pm10",
            int year = 2024) {
            var from = new DateTime(year, 1, 1);
            var to = new DateTime(year, 12, 31);

            var data = await _pulseService.GetCityAverageDataAsync(
                city, metric, from, to, avgLevel: "day"
            );

            Console.WriteLine($"Daily averages for {city}, {metric}, {year}:");

            foreach (var d in data) {
                Console.WriteLine($"{d.Stamp} → {d.Value}");
            }

            return Ok(new {
                City = city,
                Metric = metric,
                Year = year,
                Count = data.Length
            });
        }
    }
}
