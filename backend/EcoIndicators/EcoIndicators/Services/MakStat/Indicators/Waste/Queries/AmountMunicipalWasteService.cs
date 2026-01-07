using EcoIndicators.Data;
using EcoIndicators.Models.MakStat;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Services.MakStat.Indicators.Waste.Queries {
    public class AmountMunicipalWasteService  : IAmountMunicipalWasteService{
        private readonly AppDbContext _db;
        public AmountMunicipalWasteService(AppDbContext db) {
            _db = db;
        }
        public async Task<List<Amount_of_collected_municipal_waste>> GetByYearRangeAsync(int fromYear, int toYear) {
            return await _db.Amount_of_collected_municipal_wastes
                .Where(x => x.Year >= fromYear && x.Year <= toYear)
                .OrderBy(x => x.Year)
                .ToListAsync();
        }
    }
}
