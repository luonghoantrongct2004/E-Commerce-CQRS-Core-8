using AutoMapper;
using E.Domain.Entities.Users;
using E.Domain.Entities.Users.Dto;

namespace E.API.MappingProfiles;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<BasicUser, IdentityUserDto>();
        CreateMap<User, IdentityUserDto>()
            .ForMember(dest => dest.CurrentCity, opt
            => opt.MapFrom(src => src.BasicInfo.CurrentCity))
            .ForMember(dest => dest.Email, opt
            => opt.MapFrom(src => src.BasicInfo.Email))
            .ForMember(dest => dest.FullName, opt
            => opt.MapFrom(src => src.BasicInfo.FullName))
            .ForMember(dest => dest.Avatar, opt
            => opt.MapFrom(src => src.BasicInfo.Avatar))
            .ForMember(dest => dest.CreatedDate, opt
            => opt.MapFrom(src => src.BasicInfo.CreatedDate))
            .ForMember(dest => dest.UserName, opt
            => opt.MapFrom(src => src.BasicInfo.UserName));
    }
}
