using AutoMapper;
using E.API.Contracts.Categories.Responses;
using E.Domain.Entities.Categories;

namespace E.API.MappingProfiles;

public class CategoryMapping:Profile
{
    public CategoryMapping()
    {
        CreateMap<Category, CategoryResponse>();
    }
}
