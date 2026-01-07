using EcoIndicators.Data;
using EcoIndicators.Models.MakStat;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Services.MakStat.Indicators.CO2_.Queries {
    public class TotalEmissionSO2Service : ITotalEmissionsSO2Service {
        private readonly AppDbContext _db;

        public TotalEmissionSO2Service(AppDbContext db) {
            _db = db;
        }
        public async Task<List<TotalEmissionSO2>> GetByYearRangeAsync(int fromYear, int toYear) {
            return await _db.TotalEmissionSO2s
                .Where(x => x.Year >= fromYear && x.Year <= toYear)
                .OrderBy(x => x.Year)
                .ToListAsync();
        }
    }
}
