using E.Application.Models;
using E.Domain.Entities.Carts;
using MediatR;

namespace E.Application.Carts.Commands;

public class ApplyCouponCommand : IRequest<OperationResult<CartDetails>>
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public Guid CouponId { get; set; }
}