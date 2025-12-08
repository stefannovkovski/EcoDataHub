
using EcoIndicators.Services.MakStat.Indicators.Waste.Loaders;

namespace EcoIndicators.Services.MakStat.Indicators.Waste
{
    public class WasteService : IWasteService
    {
        private readonly IAmountMunicipalWaste _amountMunicipalWaste;
        private readonly IWaste_by_site_of_generation _By_Site_Of_Generation;
        private readonly ICollected_and_generated_municipal_waste _collected_And_Generated_Municipal_Waste;

        public WasteService(IAmountMunicipalWaste amountMunicipalWaste, IWaste_by_site_of_generation by_Site_Of_Generation, ICollected_and_generated_municipal_waste collected_And_Generated_Municipal_Waste)
        {
            _amountMunicipalWaste = amountMunicipalWaste;
            _By_Site_Of_Generation = by_Site_Of_Generation;
            _collected_And_Generated_Municipal_Waste = collected_And_Generated_Municipal_Waste;
        }

        public async Task SyncAllTables()
        {
            await _amountMunicipalWaste.Load();
            await _By_Site_Of_Generation.Load();
            await _collected_And_Generated_Municipal_Waste.Load();
        }
    }
}
