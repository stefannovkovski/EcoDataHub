using EcoIndicators.Models.MakStat;
namespace EcoIndicators.Services.MakStat.Indicators.Water.Queries {
    public interface IWater_abstracted_by_bussiness_Service {
        Task<List<Water_abstracted_by_business_entities>> GetByYearRangeAsync(int fromYear, int toYear);
    }
}
