using E.Application.Enums;
using E.Application.Identity;
using E.Application.Models;
using E.Application.Orders.Queries;
using E.DAL.UoW;
using E.Domain.Entities.Orders;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace E.Application.Orders.QueryHandlers;

public class GetOrdersQueryHandler
    : IRequestHandler<GetOrdersQuery, OperationResult<IEnumerable<Order>>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetOrdersQueryHandler(IReadUnitOfWork readUnitOfWork,
        IHttpContextAccessor httpContextAccessor)
    {
        _readUnitOfWork = readUnitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResult<IEnumerable<Order>>> Handle(GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<IEnumerable<Order>>();
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("IdentityId")?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
        {
            result.AddError(ErrorCode.NotFound,
                UserErrorMessage.TokenNotFound);
            return result;
        }

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            result.AddUnknownError("Invalid format in the token.");
            return result;
        }
        var order = await _readUnitOfWork.Orders.WhereAsync(
           o => o.UserId == userId);
        result.Payload = order;
        return result;
    }
}