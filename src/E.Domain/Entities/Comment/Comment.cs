using E.Domain.Entities.Comment.CommentValidators;
using E.Domain.Entities.Users;
using FluentValidation;

namespace E.Domain.Entities.Comment;

public class Comment
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string? Content { get; set; }

    public DateTime PostedAt { get; set; }
    public Guid ProductId { get; set; }
    public int StarRating { get; set; }
    public BasicUser User { get; set; }
    public static Comment CreateProductComment(Guid commentId, Guid productId, Guid userId, int rating, string content)
    {
        var validator = new CommentValidator();
        var objectToValidate = new Comment
        {
            Id = commentId,
            UserId = userId,
            Content = content,
            PostedAt = DateTime.UtcNow,
            ProductId = productId,
            StarRating = rating,
        };
        var validationResult = validator.Validate(objectToValidate);
        if (validationResult.IsValid)
        {
            return objectToValidate;
        }
        else
        {
            throw new ValidationException(validationResult.Errors);
        }
    }
}