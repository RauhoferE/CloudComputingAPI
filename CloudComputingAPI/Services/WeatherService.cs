using AutoMapper;
using CloudComputingAPI.Exceptions;
using CloudComputingAPI.Interfaces;
using CloudComputingAPI.Models;
using DataAccess.Database;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudComputingAPI.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly WeatherDbContext dbContext;

        private readonly IMapper mapper;

        public WeatherService(WeatherDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public Task<List<IdNameDto>> GetAllRegionsAsync()
        {
            return Task.FromResult(this.mapper.Map<List<IdNameDto>>(
                this.dbContext.Regions.Include(x => x.Cities).ToList()
                ));
        }

        public Task<List<WeatherDataDto>> GetAllWeatherDataAsync(int cityId)
        {
            var city = this.dbContext.Cities.FirstOrDefault(x => x.Id == cityId) ?? throw new NotFoundException("City not found!");
            var weatherData = this.dbContext.WeatherDatas
                .Include(x => x.City)
                .Include(x => x.Condition)
                .Where(wd => wd.City.Id == cityId)
                .OrderByDescending(wd => wd.Date)
                .ToList();

            return Task.FromResult(this.mapper.Map<List<WeatherDataDto>>( weatherData));
        }

        public Task<List<IdNameDto>> GetCitiesByRegionAsync(int regionId)
        {
            var region = this.dbContext.Regions.FirstOrDefault(x => x.Id == regionId) ?? throw new NotFoundException("Region not found!");
            var cities = this.dbContext.Cities.Include(x => x.Region).Where(c => c.Region.Id == regionId);

            return Task.FromResult(this.mapper.Map<List<IdNameDto>>( cities.ToList()));
        }

        public Task<WeatherDataDto> GetLatestWeatherDataAsync(int cityId)
        {
            var city = this.dbContext.Cities.FirstOrDefault(x => x.Id == cityId) ?? throw new NotFoundException("City not found!");
            var weatherData = this.dbContext.WeatherDatas
                .Include(x => x.City)
                .Include(x => x.Condition)
                .Where(wd => wd.City.Id == cityId)
                .OrderByDescending(wd => wd.Date)
                .FirstOrDefault() ?? throw new NotFoundException("City not found!");


            return Task.FromResult(this.mapper.Map<WeatherDataDto>( weatherData));
        }
    }
}
