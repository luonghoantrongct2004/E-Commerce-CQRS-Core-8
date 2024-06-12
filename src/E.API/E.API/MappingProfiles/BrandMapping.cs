using AutoMapper;
using E.API.Contracts.Brands.Responses;
using E.Domain.Entities.Brand;

namespace E.API.MappingProfiles;

public class BrandMapping : Profile
{
    public BrandMapping()
    {
        CreateMap<Brand, BrandResponse>();
    }
}