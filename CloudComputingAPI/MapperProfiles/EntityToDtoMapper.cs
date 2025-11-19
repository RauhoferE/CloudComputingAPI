

using AutoMapper;
using CloudComputingAPI.Models;
using DataAccess.Entities;

namespace CloudComputingAPI.MapperProfiles
{
    public class EntityToDtoMapper : Profile
    {
        public EntityToDtoMapper()
        {
            CreateMap<Condition, ConditionDto>();
            CreateMap<WeatherData, WeatherDataDto>();
            CreateMap<City, IdNameDto>();
            CreateMap<Region, IdNameDto>();
        }
    }
}
