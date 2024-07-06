namespace E.Domain.Entities;

public abstract class ActiveEntity : BaseEntity
{
    public bool IsActive { get; set; }
    protected ActiveEntity() : base()
    {
        IsActive = true;
    }
}