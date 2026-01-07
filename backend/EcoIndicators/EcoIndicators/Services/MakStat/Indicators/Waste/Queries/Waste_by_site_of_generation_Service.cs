using EcoIndicators.Data;
using EcoIndicators.Models.MakStat;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Services.MakStat.Indicators.Waste.Queries {
    public class Waste_by_site_of_generation_Service : IWaste_by_site_of_generations_Service {
       private readonly AppDbContext _db;
        public Waste_by_site_of_generation_Service(AppDbContext db) {
            _db = db;
        }
        public async Task<List<Waste_by_site_of_generation>> GetByYearRangeAsync(int fromYear, int toYear) {
            return await _db.Waste_by_site_of_generations
                .Where(x => x.Year >= fromYear && x.Year <= toYear)
                .OrderBy(x => x.Year)
                .ToListAsync();
        }
    }
}
