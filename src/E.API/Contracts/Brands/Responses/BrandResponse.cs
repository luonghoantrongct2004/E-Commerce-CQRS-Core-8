using E.Domain.Entities;

namespace E.API.Contracts.Brands.Responses;

public class BrandResponse : BaseEntity
{
    public string BrandName { get; set; }
}
