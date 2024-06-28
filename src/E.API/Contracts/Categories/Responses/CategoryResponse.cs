using E.Domain.Entities;

namespace E.API.Contracts.Categories.Responses;

public class CategoryResponse : BaseEntity
{
    public string CategoryName { get; set; }
}
