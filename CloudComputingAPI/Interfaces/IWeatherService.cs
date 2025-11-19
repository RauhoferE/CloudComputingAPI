using CloudComputingAPI.Models;
using DataAccess.Entities;

namespace CloudComputingAPI.Interfaces
{
    public interface IWeatherService
    {
        public Task<List<RegionDto>> GetAllRegionsAsync();

        public Task<List<CityDto>> GetCitiesByRegionAsync(int regionId);

        public Task<WeatherDataDto> GetLatestWeatherDataAsync(int cityId);

        public Task<List<WeatherDataDto>> GetAllWeatherDataAsync(int cityId);
    }
}
