using E.Application.Models;
using MediatR;

namespace E.Application.Carts.Commands;

public class CartItemRemoveCommand : IRequest<OperationResult<bool>>
{
    public Guid CartDetailsId { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
}