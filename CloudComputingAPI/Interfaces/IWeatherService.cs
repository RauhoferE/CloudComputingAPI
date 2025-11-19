using CloudComputingAPI.Models;
using DataAccess.Entities;

namespace CloudComputingAPI.Interfaces
{
    public interface IWeatherService
    {
        public Task<List<IdNameDto>> GetAllRegionsAsync();

        public Task<List<IdNameDto>> GetCitiesByRegionAsync(int regionId);

        public Task<WeatherDataDto> GetLatestWeatherDataAsync(int cityId);

        public Task<List<WeatherDataDto>> GetAllWeatherDataAsync(int cityId);
    }
}
