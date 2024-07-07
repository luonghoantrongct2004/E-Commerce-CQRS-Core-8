using E.Application.Enums;
using E.Application.Identity;
using E.Application.Models;
using E.Application.Orders.Queries;
using E.DAL.UoW;
using E.Domain.Entities.Orders;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace E.Application.Orders.QueryHandlers;

public class GetOrderQueryHandler
    : IRequestHandler<GetOrderQuery, OperationResult<Order>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetOrderQueryHandler(IReadUnitOfWork readUnitOfWork,
        IHttpContextAccessor httpContextAccessor)
    {
        _readUnitOfWork = readUnitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResult<Order>> Handle(GetOrderQuery request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<Order>();
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
        var order = await _readUnitOfWork.Orders.FirstOrDefaultAsync(
            o => o.UserId == userId && o.Id == request.Id);
        if (order is null)
        {
            result.AddError(ErrorCode.NotFound,
                OrderErrorMessage.OrderNotFound);
            return result;
        }
        result.Payload = order;

        return result;
    }
}