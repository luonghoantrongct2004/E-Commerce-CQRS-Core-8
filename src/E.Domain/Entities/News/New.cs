namespace E.Domain.Entities.News;

public class New : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
    public List<string>? Image { get; set; }
    public DateTime PublishedAt { get; set; }
    public New()
    {
        Image = new List<string>();
    }
}