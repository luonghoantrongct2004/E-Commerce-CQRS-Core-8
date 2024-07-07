using E.Application.Models;
using MediatR;

namespace E.Application.Identity.Commands;

public class DisableUserCommand : IRequest<OperationResult<bool>>
{
    public Guid IdentityUserId { get; set; }
}