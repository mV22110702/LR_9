using System.Text.Json.Serialization;

namespace LR_9.Models
{
    public class GeocodingResult
    {
        public string Name { get; set; } = String.Empty;
        public double Lat { get; set; }
        public double Lon { get; set; }
        [JsonPropertyName("country")]
        public string CountryCodeName { get; set; } = String.Empty;
        [JsonPropertyName("state")]
        public string StateName { get; set; } = String.Empty;
    }
}
