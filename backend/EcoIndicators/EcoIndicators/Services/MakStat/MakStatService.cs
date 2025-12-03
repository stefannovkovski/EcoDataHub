using EcoIndicators.Services.MakStat.Indicators.CO2;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace EcoIndicators.Services.MakStat.MakStat { 
public class MakStatService : IMakStatService
{

    private readonly ICo2Service _co2Service;
        public MakStatService(ICo2Service co2Service)
        {
            _co2Service = co2Service;
        }

        public async Task LoadData()
        {
            await _co2Service.SyncAllTables();
        }
    }
}