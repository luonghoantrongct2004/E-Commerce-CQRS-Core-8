using E.Application.Models;
using E.Domain.Entities.Brand;
using MediatR;

namespace E.Application.Brands.Queries;

public class GetBrandsQuery : IRequest<OperationResult<IEnumerable<Brand>>>
{
}