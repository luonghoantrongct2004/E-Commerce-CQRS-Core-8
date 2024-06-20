using E.Application.Models;
using E.Domain.Entities.Users.Dto;
using MediatR;

namespace E.Application.Identity.Queries;

public class LoginQuery : IRequest<OperationResult<IdentityUserDto>>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}