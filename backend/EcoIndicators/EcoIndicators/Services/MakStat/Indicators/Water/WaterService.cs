
using EcoIndicators.Models.MakStat;
using EcoIndicators.Services.MakStat.Indicators.CO2.Loaders;
using EcoIndicators.Services.MakStat.Indicators.Water.Loaders;

namespace EcoIndicators.Services.MakStat.Indicators.Water
{
    public class WaterService : IWaterService
    {
        private readonly IWaterForProductionPurposes _waterForProductionPurposes;

        public WaterService(IWaterForProductionPurposes waterForProductionPurposes)
        {
            _waterForProductionPurposes = waterForProductionPurposes;
        }
        public async Task SyncAllTables()
        {
            await _waterForProductionPurposes.Load();
        }
    }
}
