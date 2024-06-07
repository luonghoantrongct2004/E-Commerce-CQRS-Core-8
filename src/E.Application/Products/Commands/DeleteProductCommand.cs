using E.Application.Models;
using E.Domain.Entities.Products;
using MediatR;

namespace E.Application.Products.Commands;

public class DeleteProductCommand:IRequest<OperationResult<Product>>
{
    public Guid ProductId { get; set; }
    public Guid CategoryId { get; set; }

    public Guid BrandId { get; set; }
    public Guid CommentId { get; set; }
}