using AutoMapper;
using E.API.Contracts.Identities;
using E.Application.Identity.Commands;
using E.Domain.Entities.Users;
using E.Domain.Entities.Users.Dto;

namespace E.API.MappingProfiles;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<UserRegister, RegisterUserCommand>();
        CreateMap<BasicUser, IdentityUserDto>();
        CreateMap<IdentityUserDto, IdentityUser>();
        CreateMap<Login, LoginCommand>();
        CreateMap<BasicUser, IdentityUserDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.CurrentCity, opt => opt.MapFrom(src => src.CurrentCity))
            .ForMember(dest => dest.Token, opt => opt.Ignore());
    }
}
