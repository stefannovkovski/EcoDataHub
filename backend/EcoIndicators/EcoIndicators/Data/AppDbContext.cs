using EcoIndicators.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<SectorCO2> SectorCO2s { get; set; }
        public DbSet<TotalEmissionCO2> TotalEmissionCO2s { get; set; }
        public DbSet<TotalEmissionSO2> TotalEmissionSO2s { get; set; }

    }
}
