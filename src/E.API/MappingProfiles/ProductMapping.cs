using AutoMapper;
using E.API.Contracts.Products.Responses;
using E.Domain.Entities.Products;

namespace E.API.MappingProfiles;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<Product, ProductResponse>();
    }
}