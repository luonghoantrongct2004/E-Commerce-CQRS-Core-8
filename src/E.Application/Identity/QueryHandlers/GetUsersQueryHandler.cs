using E.Application.Identity.Queries;
using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Users;
using MediatR;

namespace E.Application.Identity.QueryHandlers;

public class GetAllUserQueryHandler
    : IRequestHandler<GetAllUserQuery, OperationResult<IEnumerable<UserMongo>>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public GetAllUserQueryHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task<OperationResult<IEnumerable<UserMongo>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<IEnumerable<UserMongo>>();
        result.Payload = await _readUnitOfWork.Users.GetAllAsync();
        return result;
    }
}