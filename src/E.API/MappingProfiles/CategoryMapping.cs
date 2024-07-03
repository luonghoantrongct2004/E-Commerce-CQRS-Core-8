using E.API.Contracts.Categories.Requests;
using E.API.Contracts.Categories.Responses;

namespace E.API.MappingProfiles;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        CreateMap<Category, CategoryResponse>();
        CreateMap<CategoryCreate, CreateCategoryCommand>();
    }
}