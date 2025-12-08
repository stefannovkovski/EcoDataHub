using EcoIndicators.Services.MakStat.Indicators.CO2;
using EcoIndicators.Services.MakStat.Indicators.Waste;
using EcoIndicators.Services.MakStat.Indicators.Water;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace EcoIndicators.Services.MakStat.MakStat { 
public class MakStatService : IMakStatService
{

    private readonly ICo2Service _co2Service;
    private readonly IWaterService _waterService;
        private readonly IWasteService _wasteService;
        public MakStatService(ICo2Service co2Service, IWaterService waterService,IWasteService wasteService)
        {
            _co2Service = co2Service;
            _waterService = waterService;
            _wasteService = wasteService;
        }

        public async Task LoadData()
        {
            await _co2Service.SyncAllTables();
            await _waterService.SyncAllTables();
            await _wasteService.SyncAllTables();
        }
    }
}