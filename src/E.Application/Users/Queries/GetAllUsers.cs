using E.Domain.Entities.Users;
using MediatR;

namespace E.Application.Users.Queries;

public class GetAllUsers : IRequest<IEnumerable<BasicUser>>
{
}