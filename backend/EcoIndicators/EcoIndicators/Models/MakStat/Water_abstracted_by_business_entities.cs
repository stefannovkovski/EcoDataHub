namespace EcoIndicators.Models.MakStat
{
    public class Water_abstracted_by_business_entities
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string WaterSourceType { get; set; } 
        public decimal? Total { get; set; }
        public decimal? MiningAndQuarrying { get; set; } 
        public decimal? ManufacturingIndustry { get; set; } 
        public decimal? ElectricityGasSupply { get; set; } 
        public decimal? AgricultureForestryFishing { get; set; } 
        public decimal? WaterSupplyWasteManagement { get; set; }
        public decimal? Construction { get; set; } 
    }
}
