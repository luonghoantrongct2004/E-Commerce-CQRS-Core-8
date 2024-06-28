using MediatR;

namespace E.Application.Identity.EventHandlers;

public class UserRemoveEvent : INotification
{
    public Guid IdentityUserId { get; set; }

    public UserRemoveEvent(Guid identityUserId)
    {
        IdentityUserId = identityUserId;
    }
}