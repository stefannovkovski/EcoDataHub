using EcoIndicators.Services.MakStat.Indicators.CO2;
using EcoIndicators.Services.MakStat.Indicators.CO2_.Queries;
using EcoIndicators.Services.MakStat.Indicators.Waste;
using EcoIndicators.Services.MakStat.Indicators.Waste.Queries;
using EcoIndicators.Services.MakStat.Indicators.Water;
using EcoIndicators.Services.MakStat.Indicators.Water.Loaders;
using EcoIndicators.Services.MakStat.Indicators.Water.Queries;
using EcoIndicators.Services.MakStat.Mappers;

namespace EcoIndicators.Services.MakStat.MakStat {
    public class MakStatService : IMakStatService {

        private readonly ICo2Service _co2Service;
        private readonly IWaterService _waterService;
        private readonly IWasteService _wasteService;

        private readonly IWaterForProductionPurposesService _waterForProductionPurposesService;
        private readonly IWaterBusinessPurposesService _waterBusinessPurposeService;
        private readonly IPublic_water_supply_Service _publicWaterSupplyService;
        private readonly IWater_abstracted_by_bussiness_Service _waterAbstractedService;
        private readonly IWasteWaterService _wasteWaterService;

        private readonly ICo2BySectorService _co2BySectorServie;
        private readonly ITotalEmissionCO2Service _totalEmissionCO2Service;
        private readonly ITotalEmissionsSO2Service _totalEmissionSO2Service;

        private readonly IWaste_by_site_of_generations_Service _waste_By_Site_Of_Generations_Service;
        private readonly IAmountMunicipalWasteService _amountMunicipalWasteService;
        private readonly ICollectedAndGeneratedWasteService collectedAndGeneratedWasteService;


      public MakStatService(
            ICo2Service co2Service,
            IWaterService waterService,
            IWasteService wasteService,
            IWaterForProductionPurposesService waterForProductionPurposesService,
            IWaterBusinessPurposesService waterBusinessPurposeService,
            IPublic_water_supply_Service publicWaterSupplyService,
            IWater_abstracted_by_bussiness_Service waterAbstractedService,
            IWasteWaterService wasteWaterService,
            ICo2BySectorService co2BySectorServie,
            ITotalEmissionCO2Service totalEmissionCO2Service,
            ITotalEmissionsSO2Service totalEmissionSO2Service,
            IWaste_by_site_of_generations_Service waste_By_Site_Of_Generations_Service,
            IAmountMunicipalWasteService amountMunicipalWasteService,
            ICollectedAndGeneratedWasteService collectedAndGeneratedWasteService
        ) {
            _co2Service = co2Service;
            _waterService = waterService;
            _wasteService = wasteService;
            _waterForProductionPurposesService = waterForProductionPurposesService;
            _waterBusinessPurposeService = waterBusinessPurposeService;
            _publicWaterSupplyService = publicWaterSupplyService;
            _waterAbstractedService = waterAbstractedService;
            _wasteWaterService = wasteWaterService;
            _co2BySectorServie = co2BySectorServie;
            _totalEmissionCO2Service = totalEmissionCO2Service;
            _totalEmissionSO2Service = totalEmissionSO2Service;
            _waste_By_Site_Of_Generations_Service = waste_By_Site_Of_Generations_Service;
            _amountMunicipalWasteService = amountMunicipalWasteService;
            this.collectedAndGeneratedWasteService = collectedAndGeneratedWasteService;
        }
        public async Task LoadData() {
            await _co2Service.SyncAllTables();
            await _waterService.SyncAllTables();
            await _wasteService.SyncAllTables();
        }
        public async Task<object?> GetTableAsync(string table, int fromYear, int toYear) {
            return table switch {
                // =========================
                // WATER
                // =========================
                "Public_water_supplys" =>
                    TableMapper.Map(await _publicWaterSupplyService
                        .GetByYearRangeAsync(fromYear, toYear)),

                "Waste_waters" =>
                    TableMapper.Map(await _wasteWaterService
                        .GetByYearRangeAsync(fromYear, toYear)),

                "Water_For_Productions" =>
                    TableMapper.Map(await _waterForProductionPurposesService
                        .GetByYearRangeAsync(fromYear, toYear)),

                "Water_abstracted_by_business_entitless" =>
                    TableMapper.Map(await _waterAbstractedService
                        .GetByYearRangeAsync(fromYear, toYear)),

                "Water_supplied_by_business_entitless" =>
                    TableMapper.Map(await _waterBusinessPurposeService
                        .GetByYearRangeAsync(fromYear, toYear)),

                // =========================
                // WASTE
                // =========================
                "Waste_by_site_of_generations" =>
                    TableMapper.Map(await _waste_By_Site_Of_Generations_Service
                        .GetByYearRangeAsync(fromYear, toYear)),

                "Amount_of_collected_municipal_wastes" =>
                    TableMapper.Map(await _amountMunicipalWasteService
                        .GetByYearRangeAsync(fromYear, toYear)),

                "Collected_and_generated_municipal_wastes" =>
                    TableMapper.Map(await collectedAndGeneratedWasteService
                        .GetByYearRangeAsync(fromYear, toYear)),

                // =========================
                // EMISSIONS
                // =========================
                "SectorCO2s" =>
                    TableMapper.Map(await _co2BySectorServie
                        .GetByYearRangeAsync(fromYear, toYear)),

                "TotalEmissionCO2s" =>
                    TableMapper.Map(await _totalEmissionCO2Service
                        .GetByYearRangeAsync(fromYear, toYear)),

                "TotalEmissionSO2s" =>
                    TableMapper.Map(await _totalEmissionSO2Service
                        .GetByYearRangeAsync(fromYear, toYear)),

                // =========================
                // DEFAULT
                // =========================
                _ => null
            };
        }

    }
}