using E.Domain.Entities.Brand;

namespace E.Application.Services.BrandServices;

public class BrandService
{
    private readonly BrandValidationService _validationService;

    public BrandService(BrandValidationService validationService)
    {
        _validationService = validationService;
    }

    public Brand CreateBrand(string brandName)
    {
        var objectToValidate = new Brand
        {
            Id = Guid.NewGuid(),
            BrandName = brandName,
        };
        _validationService.ValidateAndThrow(objectToValidate);

        return objectToValidate;
    }

    public void UpdateBrand(Brand brand,string brandName)
    {
        brand.BrandName = brandName;
        _validationService.ValidateAndThrow(brand);
    }
    public void DisableBrand(Brand brand)
    {
        brand.IsActive = false;
        _validationService.ValidateAndThrow(brand);
    }
}