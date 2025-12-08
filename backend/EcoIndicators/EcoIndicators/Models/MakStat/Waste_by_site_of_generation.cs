namespace EcoIndicators.Models.MakStat
{
    public class Waste_by_site_of_generation
    {
        public int Id { get; set; }
        public int Year { get; set; }

        public decimal Total { get; set; }
        public decimal Households { get; set; }
        public decimal Commercial_waste { get; set; }
    }
}
