using DataAccess.Entities;

namespace CloudComputingAPI.Interfaces
{
    public interface IWeatherService
    {
        public Task<List<Region>> GetAllRegionsAsync();

        public Task<List<City>> GetCitiesByRegionAsync(int regionId);

        public Task<WeatherData> GetLatestWeatherDataAsync(int cityId);

        public Task<List<WeatherData>> GetAllWeatherDataAsync(int cityId);
    }
}
