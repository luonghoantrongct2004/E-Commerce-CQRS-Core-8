using E.Application.Models;
using E.Domain.Entities.Brand;
using MediatR;

namespace E.Application.Brands.Queries;

public class GetBrandQuery : IRequest<OperationResult<Brand>>
{
    public Guid BrandId { get; set; }
}