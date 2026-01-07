using EcoIndicators.Models.MakStat;
namespace EcoIndicators.Services.MakStat.Indicators.Water.Queries {
    public interface IWaterBusinessPurposesService {
        Task<List<Water_supplied_by_business_entities>> GetByYearRangeAsync(int fromYear, int toYear);
    }
}
