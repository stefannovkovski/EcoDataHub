namespace EcoIndicators.Models.PulseEcoModels {
    using System.Text.Json.Serialization;

    public class EcoPulseSensorDataDto {
        [JsonPropertyName("sensorId")]
        public string SensorId { get; set; }

        [JsonPropertyName("stamp")]
        public string Stamp { get; set; }

        [JsonPropertyName("year")]
        public int? Year { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }


}
