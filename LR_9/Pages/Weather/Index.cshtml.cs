using LR_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace LR_9.Pages.Weather
{
    public class IndexModel : PageModel
    {
        public string UserInput { get; set; } = string.Empty;
        public string ServerData { get; set; } = string.Empty;

        public WeatherCast? WeatherCast { get; set; } = null;
        public string ApiErrorMessage { get; set; } = string.Empty;

        public string OpenWeatherApiKey { get; }

        private const double DEFAULT_LAT = 50.4500336;

        private const double DEFAULT_LON = 30.5241361;

        public string Units { get; set; }  = string.Empty;

        private static Coordinates DefaultCoordinates = new() { Lat = DEFAULT_LAT, Lon = DEFAULT_LON };

        public ILogger<IndexModel> Logger { get; set; }

        public IndexModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            this.OpenWeatherApiKey = configuration.GetValue<string>("OPEN_WEATHER_API_KEY");
            Logger = logger;
        }

        async public Task<PageResult> OnGetAsync(Coordinates coordinates)
        {
            if (coordinates.Lat == null)
            {
                coordinates.Lat = DefaultCoordinates.Lat;
            }
            if(coordinates.Lon == null)
            {
                coordinates.Lon = DefaultCoordinates.Lon;
            }
            Logger.LogInformation(JsonSerializer.Serialize(coordinates));
            var httpClientWeatherAPI = new HttpClient();
            httpClientWeatherAPI.DefaultRequestHeaders.Accept.Clear();
            httpClientWeatherAPI.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClientWeatherAPI.BaseAddress = new System.Uri("https://api.openweathermap.org/data/2.5/weather");
            var lat = coordinates.Lat;
            var lon = coordinates.Lon;
            Units = "metric";
            var weatherCast = await httpClientWeatherAPI.GetFromJsonAsync<WeatherCast>($"?units={Units}&lat={lat}&lon={lon}&appid={OpenWeatherApiKey}");
            if (weatherCast == null)
            {
                ApiErrorMessage = "API Error";
                return Page();
            }
            WeatherCast = weatherCast;
            Logger.LogInformation(JsonSerializer.Serialize(WeatherCast));
            return Page();
        }
    }
}
