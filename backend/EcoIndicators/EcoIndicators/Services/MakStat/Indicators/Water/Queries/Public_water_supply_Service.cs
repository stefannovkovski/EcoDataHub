using EcoIndicators.Data;
using EcoIndicators.Models.MakStat;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Services.MakStat.Indicators.Water.Queries {
    public class Public_water_supply_Service : IPublic_water_supply_Service {
        private readonly AppDbContext _db;
        public Public_water_supply_Service(AppDbContext db) {
            _db = db;
        }
        public async Task<List<Public_water_supply>> GetByYearRangeAsync(int fromYear, int toYear) {
            return await _db.Public_water_supplys
                .Where(x => x.Year >= fromYear && x.Year <= toYear)
                .OrderBy(x => x.Year)
                .ToListAsync();
        }
    }
}

