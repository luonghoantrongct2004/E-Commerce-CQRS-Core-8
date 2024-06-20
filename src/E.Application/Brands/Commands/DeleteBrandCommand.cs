using E.Application.Models;
using E.Domain.Entities.Brand;
using MediatR;

namespace E.Application.Brands.Commands;

public class DeleteBrandCommand : IRequest<OperationResult<Brand>>
{
    public Guid BrandId { get; set; }
}