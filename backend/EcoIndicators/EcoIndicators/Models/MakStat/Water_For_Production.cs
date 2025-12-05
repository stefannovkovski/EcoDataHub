using System.Diagnostics.Metrics;

namespace EcoIndicators.Models.MakStat
{
    public class Water_For_Production
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public decimal Total { get; set; }
        public decimal Fresh_water_tech  { get; set; }
        public decimal Fresh_drinking { get; set; }
        public decimal Total_recirculation_water { get; set; }
        public decimal Recurculation_fresh_water_added { get; set; }
        public decimal Reused_water_afterPurifying { get; set; }
        public decimal Reused_water_afterCooling { get; set; }

    }
}
