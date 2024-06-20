using E.Application.Models;
using E.Domain.Entities.Brand;
using MediatR;

namespace E.Application.Brands.Queries;

public class GetBrandById : IRequest<OperationResult<Brand>>
{
    public Guid BrandId { get; set; }
}