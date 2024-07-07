using MediatR;

namespace E.Application.Identity.EventHandlers;

public class UserDisableEvent : INotification
{
    public Guid IdentityUserId { get; set; }

    public UserDisableEvent(Guid identityUserId)
    {
        IdentityUserId = identityUserId;
    }
}