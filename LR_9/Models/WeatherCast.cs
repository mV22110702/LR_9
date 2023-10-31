using System.Text.Json.Serialization;

namespace LR_9.Models
{
    public class WeatherCast
    {
        [JsonPropertyName("main")]
        public Main Main { get; set; } = new Main();
        [JsonPropertyName("coord")]
        public Coordinates Coordinates { get; set; } = new Coordinates();

        [JsonPropertyName("name")]
        public string PlaceName { get; set; } = String.Empty;
    }

    public class Main
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }
        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }

        [JsonPropertyName("temp_min")]
        public double TempMin { get; set; }
        [JsonPropertyName("temp_max")]
        public double TempMax { get; set; }
        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }
        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }

    public class Coordinates
    {
        public double? Lon { get; set; }
        public double? Lat { get; set; }
    }
}
