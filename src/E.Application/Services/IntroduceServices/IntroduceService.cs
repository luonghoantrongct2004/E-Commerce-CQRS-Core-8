using E.Domain.Entities.Introductions;

namespace E.Application.Services.IntroduceServices;

public class IntroduceService
{
    private readonly IntroduceValidationService _validationService;

    public IntroduceService(IntroduceValidationService validationService)
    {
        _validationService = validationService;
    }
    public Introduce CreateIntroduce(string title,
        string description, List<string> ImageUrl)
    {
        var objectToValidate = new Introduce
        {
            Title = title,
            Description = description,
            ImageUrl = ImageUrl,
            CreatedAt = DateTime.Now,
        };
        _validationService.ValidateAndThrow(objectToValidate);
        return objectToValidate;
    }

    public void UpdateIntroduce(Introduce Introduce, 
        string title, string description, List<string> ImageUrl)
    {
        Introduce.Title = title;
        Introduce.Description = description;
        Introduce.ImageUrl = ImageUrl;
        _validationService.ValidateAndThrow(Introduce);
    }

    public void DisableIntroduce(Introduce introduce)
    {
        introduce.IsActive = false;
        _validationService.ValidateAndThrow(introduce);
    }
}