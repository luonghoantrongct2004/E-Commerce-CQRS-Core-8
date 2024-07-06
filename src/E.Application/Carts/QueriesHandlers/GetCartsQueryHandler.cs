using E.Application.Carts.Queries;
using E.Application.Enums;
using E.Application.Models;
using E.DAL.UoW;
using E.Domain.Entities.Carts;
using MediatR;

namespace E.Application.Carts.QueriesHandlers;

public class GetCartsQueryHandler : IRequestHandler<GetCartsQuery,
    OperationResult<IEnumerable<CartDetails>>>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public GetCartsQueryHandler(IReadUnitOfWork readUnitOfWork)
    {
        _readUnitOfWork = readUnitOfWork;
    }

    public async Task<OperationResult<IEnumerable<CartDetails>>> Handle(GetCartsQuery request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<IEnumerable<CartDetails>>();
        var cart = await _readUnitOfWork.Carts.WhereAsync(
            c => c.UserId == request.UserId);
        if (cart is null)
        {
            result.AddError(ErrorCode.NotFound,
                CartErrorMessage.CartNotFound);
            return result;
        }
        result.Payload = cart;
        return result;
    }
}