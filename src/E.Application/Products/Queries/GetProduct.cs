using E.Application.Models;
using E.Domain.Entities.Products;
using MediatR;

namespace E.Application.Products.Queries;

public class GetProduct : IRequest<OperationResult<Product>>
{
    public Guid ProductId { get; set; }
}