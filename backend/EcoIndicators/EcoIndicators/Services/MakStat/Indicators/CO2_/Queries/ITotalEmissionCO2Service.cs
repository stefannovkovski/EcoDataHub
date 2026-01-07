using EcoIndicators.Models.MakStat;

namespace EcoIndicators.Services.MakStat.Indicators.CO2_.Queries {
    public interface ITotalEmissionCO2Service {
        Task<List<TotalEmissionCO2>> GetByYearRangeAsync(int fromYear, int toYear);

    }
}
