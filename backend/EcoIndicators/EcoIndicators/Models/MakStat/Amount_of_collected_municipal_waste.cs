namespace EcoIndicators.Models.MakStat
{
    public class Amount_of_collected_municipal_waste
    {
        public int Id { get; set; }
        public int Year { get; set; }

        public decimal Total { get; set; }
        public decimal Paper { get; set; }
        public decimal Glass { get; set; }
        public decimal Plastic { get; set; }
        public decimal Metal_iron_steel_aluminum { get; set; }
        public decimal Organic_waste_food_leaves { get; set; }
        public decimal Textile { get; set; }
        public decimal Rubber { get; set; }
        public decimal Mixed_municipal_waste { get; set; }
        public decimal Other { get; set; }


    }
}
