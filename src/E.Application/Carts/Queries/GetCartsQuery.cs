using E.Application.Models;
using E.Domain.Entities.Carts;
using MediatR;

namespace E.Application.Carts.Queries;

public class GetCartsQuery : IRequest<OperationResult<IEnumerable<CartDetails>>>
{
    public Guid UserId { get; set; }
}