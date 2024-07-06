using E.Application.Models;
using E.Domain.Entities.Orders;
using MediatR;

namespace E.Application.Orders.Queries;

public class GetOrdersQuery : IRequest<OperationResult<IEnumerable<Order>>>
{
    public Guid UserId { get; set; }

    public GetOrdersQuery(Guid userId)
    {
        UserId = userId;
    }
}