using AutoMapper;
using E.Application.Products.Commands;
using E.Domain.Entities.Products;

namespace E.API.MappingProfiles;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<CreateProductCommand, Product>();
    }
}