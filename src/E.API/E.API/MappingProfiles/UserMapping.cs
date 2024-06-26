using AutoMapper;
using E.API.Contracts.Identities;
using E.API.Contracts.Users.Responses;
using E.Application.Identity.Commands;
using E.Domain.Entities.Users;
using E.Domain.Entities.Users.Dto;

namespace E.API.MappingProfiles;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<UserRegister, RegisterUserCommand>();
        CreateMap<DomainUser, IdentityUserDto>();
        CreateMap<IdentityUserDto, IdentityUser>();
        CreateMap<Login, LoginCommand>();
        CreateMap<DomainUser, IdentityUserDto>();
        CreateMap<UserMongo, DomainUser>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        CreateMap<DomainUser, IdentityUserDto>();
        CreateMap<UserMongo, IdentityUserDto>();
        CreateMap<UserMongo, UserResponse>();
    }
}
