using LR_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace LR_9.Pages.Weather
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "City name is required")]
        public string? InputCityName { get; set; } = null;

        public ILogger<IndexModel> Logger { get; set; }
        public string OpenWeatherApiKey { get; }

        public IndexModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            this.OpenWeatherApiKey = configuration.GetValue<string>("OPEN_WEATHER_API_KEY");
            Logger = logger;
        }


        async public Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var httpClientWeatherAPI = new HttpClient();
            httpClientWeatherAPI.DefaultRequestHeaders.Accept.Clear();
            httpClientWeatherAPI.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClientWeatherAPI.BaseAddress = new System.Uri("http://api.openweathermap.org/geo/1.0/direct");
            var matchLimit = 1;
            try
            {
                var cities = await httpClientWeatherAPI.GetFromJsonAsync<List<GeocodingResult>>($"?q={InputCityName}&limit={matchLimit}&appid={OpenWeatherApiKey}");
                if (cities == null)
                {
                    throw new InvalidOperationException("Geocoding API Error");
                }
                if (cities.Count == 0)
                {
                    throw new InvalidOperationException("Invalid city!");
                }
                var city = cities.First();
                return RedirectToPage("WeatherInfo", new { lat = city.Lat, lon = city.Lon } );
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(nameof(InputCityName), exception.Message);
                return Page();
            }

        }
    }
}
