using AutoMapper;
using E.API.Contracts.Brands.Requests;
using E.API.Contracts.Brands.Responses;
using E.Application.Brands.Commands;
using E.Domain.Entities.Brand;

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