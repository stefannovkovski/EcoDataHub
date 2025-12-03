using EcoIndicators.Data;
using EcoIndicators.Models;
using EcoIndicators.Services;
using EcoIndicators.Services.MakStat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Controllers
{
    [ApiController]
    [Route("admin/data")]
    public class AdminDataController : ControllerBase
    {
        private readonly IMakStatService _apiClient;

        public AdminDataController(IMakStatService apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpPost("populate")]
        public async Task<IActionResult> PopulateData()
        {
            await _apiClient.LoadData();
            return Ok("All tables populated successfully.");
        }
    }
}