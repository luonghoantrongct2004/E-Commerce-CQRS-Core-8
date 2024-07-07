using E.Application.Models;
using E.Domain.Entities.Orders;
using MediatR;

namespace E.Application.Orders.Queries;

public class GetOrdersQuery : IRequest<OperationResult<IEnumerable<Order>>>
{
}