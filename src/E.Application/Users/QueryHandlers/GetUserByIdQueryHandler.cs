using E.Application.Users.Queries;
using E.Domain.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E.Application.Users.QueryHandlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserById, BasicUser>
{
    private readonly AppDbContext _context;

    public GetUserByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BasicUser> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserId == request.UserId);
    }
}