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
    }
}
