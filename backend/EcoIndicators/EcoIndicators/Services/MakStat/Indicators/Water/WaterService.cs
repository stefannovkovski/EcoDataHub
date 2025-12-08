
using EcoIndicators.Models.MakStat;
using EcoIndicators.Services.MakStat.Indicators.CO2.Loaders;
using EcoIndicators.Services.MakStat.Indicators.Water.Loaders;

namespace EcoIndicators.Services.MakStat.Indicators.Water
{
    public class WaterService : IWaterService
    {
        private readonly IWaterForProductionPurposes _waterForProductionPurposes;
        private readonly IWaterBusinessPurpose _waterBusinessPurpose;
        private readonly IPublic_water_supply _publicWaterSupply;
        private readonly IWater_abstracted_by_business _waterAbstracted;
        private readonly IWasteWater _wasteWater;
        public WaterService(IWaterForProductionPurposes waterForProductionPurposes, IWaterBusinessPurpose waterBusinessPurpose, IPublic_water_supply publicWaterSupply, IWater_abstracted_by_business waterAbstracted, IWasteWater wasteWater)
        {
            _waterForProductionPurposes = waterForProductionPurposes;
            _waterBusinessPurpose = waterBusinessPurpose;
            _publicWaterSupply = publicWaterSupply;
            _waterAbstracted = waterAbstracted;
            _wasteWater = wasteWater;
        }
        public async Task SyncAllTables()
        {
            await _waterForProductionPurposes.Load();
            await _waterBusinessPurpose.Load();
            await _publicWaterSupply.Load();
            await _waterAbstracted.Load();
            await _wasteWater.Load();
        }
    }
}
