using E.Application.Models;
using E.Domain.Entities;
using E.Domain.Entities.Brand;
using MediatR;

namespace E.Application.Brands.Queries;

public class GetBrandQuery : BaseEntity, IRequest<OperationResult<Brand>>
{
}