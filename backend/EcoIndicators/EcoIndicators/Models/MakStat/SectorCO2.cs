using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EcoIndicators.Models.MakStat
{
    public class SectorCO2
    {
        public int Id { get; set; }
        public int Year { get; set; }

        public decimal Energy { get; set; }
        public decimal Heat { get; set; }
        public decimal Transport { get; set; }
        public decimal Industrial_processes { get; set; }
        public decimal Waste { get; set; }
        public decimal Agriculture { get; set; }
        public decimal Total { get; set; }

    }
}
