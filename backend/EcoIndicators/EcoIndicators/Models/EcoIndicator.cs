namespace EcoIndicators.Models
{
    public class EcoIndicator
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public double Value { get; set; }
        public string Unit { get; set; } = string.Empty;
        public DateTime RecordedDate { get; set; }
        public string? Region { get; set; }
    }
}
