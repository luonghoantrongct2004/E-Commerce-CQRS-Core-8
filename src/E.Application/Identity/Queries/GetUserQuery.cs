using E.Application.Models;
using E.Domain.Entities.Users;
using MediatR;

namespace E.Application.Identity.Queries;

public class GetUserQuery : IRequest<OperationResult<UserMongo>>
{
    public Guid UserId { get; set; }
}