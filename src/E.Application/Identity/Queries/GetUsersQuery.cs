using E.Application.Models;
using E.Domain.Entities.Users;
using MediatR;

namespace E.Application.Identity.Queries;

public class GetAllUserQuery : IRequest<OperationResult<IEnumerable<UserMongo>>>
{
}