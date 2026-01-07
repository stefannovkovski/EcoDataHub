using EcoIndicators.Models.MakStat;

namespace EcoIndicators.Services.MakStat.Indicators.Waste.Queries {
    public interface ICollectedAndGeneratedWasteService {
        Task<List<Collected_and_generated_municipal_waste>> GetByYearRangeAsync(int fromYear, int toYear);
    }
}
