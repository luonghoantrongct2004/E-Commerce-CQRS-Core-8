using E.API.Contracts.Brands.Requests;
using E.API.Contracts.Brands.Responses;

namespace E.API.MappingProfiles;

public class BrandMapping : Profile
{
    public BrandMapping()
    {
        CreateMap<Brand, BrandResponse>();
        CreateMap<BrandCreate, CreateBrandCommand>();
        CreateMap<BrandUpdate, UpdateBrandCommand>();
    }
}