using EcoIndicators.Data;
using EcoIndicators.Models.MakStat;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Services.MakStat.Indicators.Water.Queries {
    public class Water_abstracted_by_bussiness_Service : IWater_abstracted_by_bussiness_Service {
        private readonly AppDbContext _db;
        public Water_abstracted_by_bussiness_Service(AppDbContext db) {
            _db = db;
        }
        public async Task<List<Water_abstracted_by_business_entities>> GetByYearRangeAsync(int fromYear, int toYear) {
            return await _db.Water_abstracted_by_business_entitiess
                .Where(x => x.Year >= fromYear && x.Year <= toYear)
                .OrderBy(x => x.Year)
                .ToListAsync();
        }
    }
}
