using EcoIndicators.Services.MakStat.Indicators.CO2.Loaders;

namespace EcoIndicators.Services.MakStat.Indicators.CO2
{
    public class Co2Service : ICo2Service
    {
        private readonly ICo2BySectorLoader _sectorLoader;
        private readonly ITotalEmissionCO2Loader _totalEmissionC02;
        private readonly ITotalEmissionSO2Loader _totalEmissionS02;


        public Co2Service(ICo2BySectorLoader sectorLoader, ITotalEmissionCO2Loader totalEmissionC02, ITotalEmissionSO2Loader totalEmissionS02)
        {
            _sectorLoader = sectorLoader;
            _totalEmissionC02 = totalEmissionC02;
            _totalEmissionS02 = totalEmissionS02;
        }

        public async Task SyncAllTables()
        {
            await _sectorLoader.Load();
            await _totalEmissionC02.Load();
            await _totalEmissionS02.Load();
        }
    }

}
