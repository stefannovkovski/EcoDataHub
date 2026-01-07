using EcoIndicators.Data;
using EcoIndicators.Models.MakStat;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Services.MakStat.Indicators.Waste.Queries {
    public class CollectedAndGeneratedWasteService : ICollectedAndGeneratedWasteService {
        private readonly AppDbContext _db;
        public CollectedAndGeneratedWasteService(AppDbContext db) {
            _db = db;
        }
        public async Task<List<Collected_and_generated_municipal_waste>> GetByYearRangeAsync(int fromYear, int toYear) {
            return await _db.Collected_and_generated_municipal_wastes
                .Where(x => x.Year >= fromYear && x.Year <= toYear)
                .OrderBy(x => x.Year)
                .ToListAsync();
        }
    }
}
