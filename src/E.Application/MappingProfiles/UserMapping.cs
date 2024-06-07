using AutoMapper;
using E.Application.Users.Commands;
using E.Domain.Entities.Users;

namespace E.Application.MappingProfiles;

public class UserMapping:Profile
{
    public UserMapping()
    {
        CreateMap<CreateUserCommands, BasicUser>();
    }
}