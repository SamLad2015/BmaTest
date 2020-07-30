using System.Linq;
using AutoMapper;
using BmaTestApi.Dtos;
using BmaTestApi.Entities;

namespace BmaTestApi.MappingProfiles
{
    public class RestaurantMappings : Profile
    {
        public RestaurantMappings()
        {
            CreateMap<RestaurantEntity, RestaurantDto>()
                .ForMember(obj => obj.Cuisine, 
                    opt => 
                        opt.MapFrom(src => 
                            src.Cuisine.Select(c => c.CuisineEntity.Name).ToList()))
                .ReverseMap();
            CreateMap<RestaurantRequestDto, RestaurantEntity>();
        }
    }
}
