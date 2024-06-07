using E.Application.Users.Queries;
using E.Domain.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E.Application.Users.QueryHandlers;

public class GetAllUserQueryHandlers : IRequestHandler<GetAllUsers, IEnumerable<BasicUser>>
{
    private readonly AppDbContext _context;

    public GetAllUserQueryHandlers(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BasicUser>> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        return await _context.Users.ToListAsync();
    }
}