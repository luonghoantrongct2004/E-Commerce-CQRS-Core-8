using E.API.Contracts.Products.Requests;
using E.API.Contracts.Products.Responses;

namespace E.API.MappingProfiles;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<Product, ProductResponse>();
        CreateMap<ProductCreate, CreateProductCommand>();
        CreateMap<ProductUpdate, UpdateProductCommand>();
    }
}