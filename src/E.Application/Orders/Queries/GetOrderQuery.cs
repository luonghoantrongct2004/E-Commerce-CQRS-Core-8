using E.Application.Models;
using E.Domain.Entities;
using E.Domain.Entities.Orders;
using MediatR;

namespace E.Application.Orders.Queries;

public class GetOrderQuery : BaseEntity, IRequest<OperationResult<Order>>
{
}