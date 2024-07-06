using E.Domain.Entities.News;

namespace E.Application.Services.NewServices;

public class NewService
{
    private readonly NewValidationService _validationService;

    public NewService(NewValidationService validationService)
    {
        _validationService = validationService;
    }
    public New CreateNew(string title,
       string content,string author, List<string> ImageUrl)
    {
        var objectToValidate = new New
        {
            Title = title,
            Content = content,
            Author = author,
            Image = ImageUrl,
            PublishedAt = DateTime.Now,
        };
        _validationService.ValidateAndThrow(objectToValidate);
        return objectToValidate;
    }

    public void UpdateNew(New news, string title,
       string content, string author, List<string> ImageUrl)
    {
        news.Title = title;
        news.Content = content;
        news.Author = author;
        news.Image = ImageUrl;

        _validationService.ValidateAndThrow(news);
    }

    public void DisableNew(New news)
    {
        news.IsActive = false;
        _validationService.ValidateAndThrow(news);
    }
}