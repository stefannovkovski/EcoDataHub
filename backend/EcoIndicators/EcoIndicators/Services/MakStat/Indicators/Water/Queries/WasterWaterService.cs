using EcoIndicators.Data;
using EcoIndicators.Models.MakStat;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Services.MakStat.Indicators.Water.Queries {
    public class WasterWaterService : IWasteWaterService{
        private readonly AppDbContext _db;
        public WasterWaterService(AppDbContext db) {
            _db = db;
        }
        public async Task<List<Waste_water>> GetByYearRangeAsync(int fromYear, int toYear) {
            return await _db.Waste_waters
                .Where(x => x.Year >= fromYear && x.Year <= toYear)
                .OrderBy(x => x.Year)
                .ToListAsync();
        }
    }
}
