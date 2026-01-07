using EcoIndicators.Models.MakStat;


namespace EcoIndicators.Services.MakStat.Indicators.Water.Queries {
    public interface IWasteWaterService {
        Task<List<Waste_water>> GetByYearRangeAsync(int fromYear, int toYear);

    }
}
