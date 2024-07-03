using E.Application.Models;
using E.Domain.Entities.Carts;
using MediatR;

namespace E.Application.Carts.Commands;

public class CartItemAddCommand : IRequest<OperationResult<CartDetails>>
{
    public Guid CartDetailsId { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}