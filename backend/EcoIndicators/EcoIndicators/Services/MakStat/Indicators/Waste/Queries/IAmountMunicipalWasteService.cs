using EcoIndicators.Models.MakStat;

namespace EcoIndicators.Services.MakStat.Indicators.Waste.Queries {
    public interface IAmountMunicipalWasteService {
        Task<List<Amount_of_collected_municipal_waste?>> GetByYearRangeAsync(int fromYear, int toYear);
    }
}
