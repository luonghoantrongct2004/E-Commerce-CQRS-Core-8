using E.Application.Models;
using MediatR;

namespace E.Application.Coupons.Commands;

public class DisableCouponCommand : IRequest<OperationResult<bool>>
{
    public Guid Id { get; set; }
}