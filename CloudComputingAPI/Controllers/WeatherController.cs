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
        public async Task<IActionResult> GetRegionsAsync()
        {
            var t = await this.weatherService.GetAllRegionsAsync();
            return Ok(t);
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetCitiesOfRegion([FromQuery]int regionId)
        {
            return Ok(await this.weatherService.GetCitiesByRegionAsync(regionId));
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestWeatherData([FromQuery]int cityId)
        {
            return Ok(await this.weatherService.GetLatestWeatherDataAsync(cityId));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllWeatherData([FromQuery]int cityId)
        {
            return Ok(await this.weatherService.GetAllWeatherDataAsync(cityId));
        }
    }
}
