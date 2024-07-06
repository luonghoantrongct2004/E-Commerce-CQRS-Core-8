using E.Application.Models;
using E.Domain.Entities.Products;
using MediatR;

namespace E.Application.Orders.Commands;

public class CancelOrderCommand : IRequest<OperationResult<bool>>
{
    public Guid Id { get; set; }
    public Product Product { get; set; }
}