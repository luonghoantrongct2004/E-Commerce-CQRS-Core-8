using E.Application.Models;
using E.Domain.Entities.Carts;
using MediatR;

namespace E.Application.Carts.Queries;

public class GetCartQuery : IRequest<OperationResult<CartDetails>>
{
    public Guid UserId { get; set; }
}