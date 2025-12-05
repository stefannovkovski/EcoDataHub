using EcoIndicators.Services.MakStat.Indicators.CO2;
using EcoIndicators.Services.MakStat.Indicators.Water;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace EcoIndicators.Services.MakStat.MakStat { 
public class MakStatService : IMakStatService
{

    private readonly ICo2Service _co2Service;
    private readonly IWaterService _waterService;

        public MakStatService(ICo2Service co2Service, IWaterService waterService)
        {
            _co2Service = co2Service;
            _waterService = waterService;
        }

        public async Task LoadData()
        {
            await _co2Service.SyncAllTables();
            await _waterService.SyncAllTables();
        }
    }
}