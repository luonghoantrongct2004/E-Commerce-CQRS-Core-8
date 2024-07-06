using E.Application.Models;
using E.Domain.Entities.Users.Dto;
using MediatR;

namespace E.Application.Identity.Commands;

public class LoginCommand : IRequest<OperationResult<IdentityUserDto>>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}