using EcoIndicators.Models.MakStat;

namespace EcoIndicators.Services.MakStat.Indicators.CO2_.Queries {
    public interface ICo2BySectorService {
       
        Task<List<SectorCO2>> GetByYearRangeAsync(int fromYear, int toYear);


    }
}
