using EcoIndicators.Data;
using EcoIndicators.Models.MakStat;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Services.MakStat.Indicators.Water.Queries {
    public class WaterBusinessPurposesService : IWaterBusinessPurposesService {
        private readonly AppDbContext _db;
        public WaterBusinessPurposesService(AppDbContext db) {
            _db = db;
        }
        public async Task<List<Water_supplied_by_business_entities>> GetByYearRangeAsync(int fromYear, int toYear) {
            return await _db.Water_supplied_by_business_entitiess
                .Where(x => x.Year >= fromYear && x.Year <= toYear)
                .OrderBy(x => x.Year)
                .ToListAsync();
        }
    }
}
