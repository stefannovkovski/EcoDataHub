namespace EcoIndicators.Models.MakStat
{
    public class TotalEmissionSO2
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public decimal Total { get; set; }
        public decimal Combustion_processes { get; set; }
        public decimal Production_processes { get; set; }
        public decimal Transport { get; set; }
        public decimal Other { get; set; }
    }
}
