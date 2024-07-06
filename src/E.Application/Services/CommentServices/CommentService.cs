using E.Domain.Entities.Comment;

namespace E.Application.Services.CommentServices;

public class CommentService
{
    private readonly CommentValidationService _validationService;

    public CommentService(CommentValidationService validationService)
    {
        _validationService = validationService;
    }

    public Comment CreateProductComment(Guid productId,
        Guid userId, int rating, string content)
    {
        var objectToValidate = new Comment
        {
            UserId = userId,
            Content = content,
            PostedAt = DateTime.UtcNow,
            ProductId = productId,
            StarRating = rating,
        };
        _validationService.ValidateAndThrow(objectToValidate);
        return objectToValidate;
    }

    public void DeleteCategory(Comment comment, Guid commentId)
    {
        comment.Id = commentId;
        _validationService.ValidateAndThrow(comment);
    }
}