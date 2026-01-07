using EcoIndicators.Services.MakStat;
using Microsoft.AspNetCore.Mvc;

namespace EcoIndicators.Controllers {
    [ApiController]
    [Route("api/makstat")]
    public class MakStatController : ControllerBase {
        private readonly IMakStatService _makStatService;
        public MakStatController(IMakStatService makStatService) {
            _makStatService = makStatService;
        }


        [HttpGet("table")]
        public async Task<IActionResult> GetTable(
           [FromQuery] string table,
           [FromQuery] int fromYear,
           [FromQuery] int toYear) {
            var result = await _makStatService.GetTableAsync(table, fromYear, toYear);

            if (result == null)
                return BadRequest("Unknown table");

            return Ok(result);
        }





    }


}