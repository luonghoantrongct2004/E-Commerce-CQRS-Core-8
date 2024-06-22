using AutoMapper;
using E.Domain.Entities.Users.Dto;
using E.Domain.Entities.Users;

namespace E.Application.MappingProfiles;

public class IdentityMapping : Profile
{
    public IdentityMapping()
    {
        CreateMap<User, IdentityUserDto>()
            .ForMember(dest => dest.CurrentCity, opt
            => opt.MapFrom(src => src.BasicInfo.CurrentCity))
            .ForMember(dest => dest.FullName, opt
            => opt.MapFrom(src => src.BasicInfo.FullName))
            .ForMember(dest => dest.Avatar, opt
            => opt.MapFrom(src => src.BasicInfo.Avatar))
            .ForMember(dest => dest.CreatedDate, opt
            => opt.MapFrom(src => src.BasicInfo.CreatedDate))
            .ForMember(dest => dest.Email, opt
            => opt.MapFrom(src => src.BasicInfo.UserName));
    }
}