using E.Domain.Entities.Users;

namespace E.Domain.Entities.Comment;

public class Comment : BaseEntity
{
    public Guid UserId { get; set; }

    public string? Content { get; set; }

    public DateTime PostedAt { get; set; }
    public Guid ProductId { get; set; }
    public int StarRating { get; set; }
    public DomainUser User { get; set; }
}