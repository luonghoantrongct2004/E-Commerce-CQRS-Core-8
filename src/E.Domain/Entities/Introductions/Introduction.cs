namespace E.Domain.Entities.Introductions;

public class Introduction : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<string>? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public Introduction()
    {
        ImageUrl = new List<string>();
        CreatedAt = DateTime.Now;
    }
}