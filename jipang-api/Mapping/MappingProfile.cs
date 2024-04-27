using AutoMapper;
using jipang.Application.DTOs.IN;
using jipang.Application.DTOs.OUT;
using jipang.Domain.Entities;

namespace jipang_api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Users, UserDtoIn>();
            CreateMap<Users, UserDtoOut>();
            CreateMap<UserDtoIn, Users>();
            CreateMap<UserDtoIn, Users>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));
        }
    }
}
