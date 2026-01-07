using EcoIndicators.Data;
using EcoIndicators.Models.MakStat;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Services.MakStat.Indicators.CO2_.Queries {
    public class Co2BySectorService : ICo2BySectorService {
        private readonly AppDbContext _db;
        public Co2BySectorService(AppDbContext db) {
            _db = db;
        }
        public async Task<List<SectorCO2>> GetByYearRangeAsync(int fromYear, int toYear) {
            return await _db.SectorCO2s
                .Where(x => x.Year >= fromYear && x.Year <= toYear)
                .OrderBy(x => x.Year)
                .ToListAsync();
        }

     
    }
}
