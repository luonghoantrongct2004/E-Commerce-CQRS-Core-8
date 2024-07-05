using E.Application.Models;
using E.Domain.Entities.Users.Dto;
using MediatR;
using System.Security.Claims;

namespace E.Application.Identity.Queries;

public class GetCurrentUserQuery : IRequest<OperationResult<IdentityUserDto>>
{
    public Guid UserProfileId { get; set; }
    public ClaimsPrincipal ClaimsPrincipal { get; set; }
}