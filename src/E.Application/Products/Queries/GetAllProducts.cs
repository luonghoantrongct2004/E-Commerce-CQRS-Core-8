using E.Application.Models;
using E.Domain.Entities.Products;
using MediatR;

namespace E.Application.Products.Queries;

public class GetAllProducts:IRequest<OperationResult<IEnumerable<Product>>>
{
}