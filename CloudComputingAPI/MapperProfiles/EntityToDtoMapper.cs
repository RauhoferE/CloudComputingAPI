

using AutoMapper;
using CloudComputingAPI.Models;
using DataAccess.Entities;

namespace CloudComputingAPI.MapperProfiles
{
    public class EntityToDtoMapper : Profile
    {
        public EntityToDtoMapper()
        {
            CreateMap<Region, RegionDto>()
                .ForMember(dest => dest.Citys, opt => opt.MapFrom(src => src.Cities));
            CreateMap<Condition, ConditionDto>();
            CreateMap<City, CityDto>();
            CreateMap<WeatherData, WeatherDataDto>();
            CreateMap<City, IdNameDto>();
        }
    }
}
