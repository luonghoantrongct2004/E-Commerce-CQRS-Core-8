using E.Application.Models;
using E.Domain.Entities.Users;
using MediatR;

namespace E.Application.Identity.Queries;

public class GetUserByIdQuery :IRequest<OperationResult<DomainUser>>
{
    public Guid UserId { get; set; }
}