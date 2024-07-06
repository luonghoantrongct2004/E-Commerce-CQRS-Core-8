using E.Application.Enums;
using E.Application.Identity.Queries;
using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Users;
using MediatR;

namespace E.Application.Identity.QueryHandlers;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, OperationResult<UserMongo>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public GetUserQueryHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task<OperationResult<UserMongo>> Handle(GetUserQuery request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<UserMongo>();
        var user = await _readUnitOfWork.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
        if (user is null)
        {
            result.AddError(ErrorCode.NotFound,
                string.Format(UserErrorMessage.UserNotFound, request.UserId));
            return result;
        }
        result.Payload = user;
        return result;
    }
}