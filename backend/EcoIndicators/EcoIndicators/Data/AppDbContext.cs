using EcoIndicators.Models.MakStat;
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
        public DbSet<Water_For_Production> Water_For_Productions { get; set; }
        public DbSet<Water_supplied_by_business_entities> Water_supplied_by_business_entitiess { get; set; }

        public DbSet<Public_water_supply> Public_water_supplys { get; set; }
        public DbSet<Water_abstracted_by_business_entities> Water_abstracted_by_business_entitiess { get; set; }
        public DbSet<Waste_water> Waste_waters { get; set; }

        public DbSet<Amount_of_collected_municipal_waste> Amount_of_collected_municipal_wastes { get; set; }
        public DbSet<Waste_by_site_of_generation> Waste_by_site_of_generations { get; set; }
        public DbSet<Collected_and_generated_municipal_waste> Collected_and_generated_municipal_wastes { get; set; }



    }
}
