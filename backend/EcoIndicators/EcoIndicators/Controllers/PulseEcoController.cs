using EcoIndicators.Models.PulseEcoModels;
using EcoIndicators.Services.PulseEco;
using Microsoft.AspNetCore.Mvc;

namespace EcoIndicators.Controllers {
    [ApiController]
    [Route("api/pulseeco")]
    public class PulseEcoController : ControllerBase {
        private readonly IPulseEcoService _pulseEcoService;
        public PulseEcoController(IPulseEcoService pulseEcoService) {
            _pulseEcoService = pulseEcoService;
        }


        [HttpGet("data/multiple")]
        public async Task<IActionResult> GetMultipleIndicators(
    [FromQuery] string city,
    [FromQuery] string[] valueTypes, 
    [FromQuery] DateTime from,
    [FromQuery] DateTime to,
    [FromQuery] string avgLevel = null,
    [FromQuery] string sensorId = null) {
            var result = new Dictionary<string, EcoPulseSensorDataDto[]>();

            foreach (var type in valueTypes) {
                var data = await _pulseEcoService.GetCityAverageDataAsync(
                    city,
                    type,
                    from,
                    to,
                    avgLevel,
                    sensorId
                );
                result[type] = data;
            }

            return Ok(result); 
        }


    }
}
