namespace EcoIndicators.Models.MakStat
{
    public class Water_supplied_by_business_entities
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public decimal Total { get; set; }
        public decimal Ground_water { get; set; }
        public decimal Springs { get; set; }
        public decimal Water_courses { get; set; }
        public decimal Reservoirs { get; set; }
        public decimal Lakes { get; set; }
    }
}
