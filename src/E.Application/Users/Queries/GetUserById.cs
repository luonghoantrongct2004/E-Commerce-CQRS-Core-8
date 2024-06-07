using E.Domain.Entities.Users;
using MediatR;

namespace E.Application.Users.Queries;

public class GetUserById : IRequest<BasicUser>
{
    public Guid UserId { get; set; }
}