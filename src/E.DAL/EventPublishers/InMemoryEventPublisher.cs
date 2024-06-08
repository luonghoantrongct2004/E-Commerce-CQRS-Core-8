
using MediatR;

namespace E.DAL.EventPublishers;

public class InMemoryEventPublisher : IEventPublisher
{
    private readonly IMediator _mediator;
    public async Task PublishAsync<TEvent>(TEvent eventMessage) where TEvent : class
    {
        await _mediator.Publish(eventMessage);
    }
}