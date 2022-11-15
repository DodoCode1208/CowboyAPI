using AutoMapper;
using CowboyAPI.Dtos;
using CowboyAPI.Models;

namespace CowboyAPI.DtoMapperProfiles
{
    public class CowboyDtoProfile : Profile
    {
        public CowboyDtoProfile()
        {
            CreateMap<Cowboy, CowboyFetchRequestDto>();
            CreateMap<CowboyCreateRequestDto, Cowboy>();
            CreateMap<CowboyUpdateRequestDto, Cowboy>();
            CreateMap<Cowboy, CowboyUpdateRequestDto>();
                
        }


    }
}
