using EcoIndicators.Models.MakStat;

namespace EcoIndicators.Services.MakStat.Indicators.Waste.Queries {
    public interface IWaste_by_site_of_generations_Service {
        Task<List<Waste_by_site_of_generation>> GetByYearRangeAsync(int fromYear, int toYear);
    }
}
