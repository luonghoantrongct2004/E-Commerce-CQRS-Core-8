namespace E.Domain.Entities.Introductions;

public class Introduce : ActiveEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<string>? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public Introduce()
    {
        ImageUrl = new List<string>();
        CreatedAt = DateTime.Now;
    }
}