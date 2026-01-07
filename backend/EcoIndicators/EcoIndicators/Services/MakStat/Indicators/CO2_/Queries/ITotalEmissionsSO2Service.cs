using EcoIndicators.Models.MakStat;

namespace EcoIndicators.Services.MakStat.Indicators.CO2_.Queries {
    public interface ITotalEmissionsSO2Service {
        Task<List<TotalEmissionSO2>> GetByYearRangeAsync(int fromYear, int toYear);

    }
}
