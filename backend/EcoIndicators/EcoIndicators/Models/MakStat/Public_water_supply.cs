namespace EcoIndicators.Models.MakStat
{
    public class Public_water_supply
    {
        public int Id { get; set; }
        public int Year { get; set; }

        public decimal Total { get; set; }
        public decimal Abstracted_water { get; set; }
        public decimal Ground_water { get; set; }
        public decimal Springs { get; set; }
        public decimal Watercourse { get; set; }
        public decimal Reservoir { get; set; }
        public decimal Lake { get; set; }
        public decimal Water_taken_from_other_water_supply_systems { get; set; }


    }
}
