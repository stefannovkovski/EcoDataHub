using EcoIndicators.Data;
using EcoIndicators.Models.MakStat;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Services.MakStat.Indicators.Water.Queries {
    public class WaterForProductionPurposesService : IWaterForProductionPurposesService {
        private readonly AppDbContext _db;
        public WaterForProductionPurposesService(AppDbContext db) {
            _db = db;
        }
         public async Task<List<Water_For_Production>> GetByYearRangeAsync(int fromYear, int toYear) {
            return await _db.Water_For_Productions
                .Where(x => x.Year >= fromYear && x.Year <= toYear)
                .OrderBy(x => x.Year)
                .ToListAsync();
        }
    }
}
