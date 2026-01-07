using EcoIndicators.Models.MakStat;

namespace EcoIndicators.Services.MakStat.Indicators.Water.Queries {
    public interface IPublic_water_supply_Service {
        Task<List<Public_water_supply>> GetByYearRangeAsync(int fromYear, int toYear);
    }
}
