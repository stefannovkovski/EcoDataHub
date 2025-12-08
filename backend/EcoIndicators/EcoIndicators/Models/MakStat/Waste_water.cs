namespace EcoIndicators.Models.MakStat
{
    public class Waste_water
    {
        public int Id { get; set; }
        public int Year { get; set; }

        public decimal Total { get; set; }
        public decimal From_households { get; set; }
        public decimal From_the_economy { get; set; }
        public decimal From_other_users { get; set; }
        public decimal From_own_consumption { get; set; }
    }
}
