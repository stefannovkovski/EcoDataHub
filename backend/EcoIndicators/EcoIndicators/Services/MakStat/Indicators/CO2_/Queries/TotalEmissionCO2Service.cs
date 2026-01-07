using EcoIndicators.Data;
using EcoIndicators.Models.MakStat;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Services.MakStat.Indicators.CO2_.Queries {
    public class TotalEmissionCO2Service : ITotalEmissionCO2Service {
        private readonly AppDbContext _db;
        public TotalEmissionCO2Service(AppDbContext db) {
            _db = db;
        }
        public async Task<List<TotalEmissionCO2>> GetByYearRangeAsync(int fromYear, int toYear) {
            return await _db.TotalEmissionCO2s
                .Where(x => x.Year >= fromYear && x.Year <= toYear)
                .OrderBy(x => x.Year)
                .ToListAsync();
        }
    }
}
