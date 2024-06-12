using E.Application.Models;
using E.Domain.Entities.Brand;
using MediatR;

namespace E.Application.Brands.Commands;

public class CreateBrandCommand:IRequest<OperationResult<Brand>>
{
    public string BrandName { get; set; }
}