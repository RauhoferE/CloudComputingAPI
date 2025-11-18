using CloudComputingAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CloudComputingAPI.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class WeatherController : Controller
    {
        private readonly IWeatherService weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        [HttpGet("regions")]
        public Task<IActionResult> GetRegionsAsync()
        {
            var regions =  this.weatherService.GetAllRegionsAsync();
            return Task.FromResult<IActionResult>(Ok(regions));
        }

        [HttpGet("cities")]
        public Task<IActionResult> GetCitiesOfRegion([FromQuery]int regionId)
        {
            var cities = this.weatherService.GetCitiesByRegionAsync(regionId);
            return Task.FromResult<IActionResult>(Ok(cities));
        }

        [HttpGet("latest-weather")]
        public Task<IActionResult> GetLatestWeatherData([FromQuery]int cityId)
        {
            var weatherData = this.weatherService.GetLatestWeatherDataAsync(cityId);
            return Task.FromResult<IActionResult>(Ok(weatherData));
        }

        [HttpGet("all-weather")]
        public Task<IActionResult> GetAllWeatherData([FromQuery]int cityId)
        {
            var weatherData = this.weatherService.GetAllWeatherDataAsync(cityId);
            return Task.FromResult<IActionResult>(Ok(weatherData));
        }
    }
}
