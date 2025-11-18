using CloudComputingAPI.Exceptions;
using CloudComputingAPI.Interfaces;
using DataAccess.Database;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudComputingAPI.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly WeatherDbContext dbContext;

        public WeatherService(WeatherDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<Region>> GetAllRegionsAsync()
        {
            return Task.FromResult(this.dbContext.Regions.ToList());
        }

        public Task<List<WeatherData>> GetAllWeatherDataAsync(int cityId)
        {
            var city = this.dbContext.Cities.FirstOrDefault(x => x.Id == cityId) ?? throw new NotFoundException("City not found!");
            var weatherData = this.dbContext.WeatherDatas
                .Include(x => x.City)
                .Include(x => x.Condition)
                .Where(wd => wd.City.Id == cityId)
                .OrderByDescending(wd => wd.Date)
                .ToList();

            return Task.FromResult(weatherData);
        }

        public Task<List<City>> GetCitiesByRegionAsync(int regionId)
        {
            var region = this.dbContext.Regions.FirstOrDefault(x => x.Id == regionId) ?? throw new NotFoundException("Region not found!");
            var cities = this.dbContext.Cities.Include(x => x.Region).Where(c => c.Region.Id == regionId);

            return Task.FromResult(cities.ToList());
        }

        public Task<WeatherData> GetLatestWeatherDataAsync(int cityId)
        {
            var city = this.dbContext.Cities.FirstOrDefault(x => x.Id == cityId) ?? throw new NotFoundException("City not found!");
            var weatherData = this.dbContext.WeatherDatas
                .Include(x => x.City)
                .Include(x => x.Condition)
                .Where(wd => wd.City.Id == cityId)
                .OrderByDescending(wd => wd.Date)
                .FirstOrDefault() ?? throw new NotFoundException("City not found!");


            return Task.FromResult(weatherData);
        }
    }
}
