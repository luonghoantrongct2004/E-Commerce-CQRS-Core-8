namespace E.DAL.EventPublishers;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent eventMessage) where TEvent : class;
}
