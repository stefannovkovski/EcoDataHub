using EcoIndicators.Models.MakStat;

namespace EcoIndicators.Services.MakStat.Indicators.Water.Queries {
    public interface IWaterForProductionPurposesService {
        Task<List<Water_For_Production>> GetByYearRangeAsync(int fromYear, int toYear);
    }
}
