using AutoMapper;
using E.Application.Products.Commands;
using E.Domain.Entities.Products;

namespace E.Application.MappingProfiles;

internal class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<CreateProductCommand, Product>();
    }
}