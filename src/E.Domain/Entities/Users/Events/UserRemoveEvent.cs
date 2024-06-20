using MediatR;

namespace E.Application.Identity.EventHandlers;

public class UserRemoveEvent : INotification
{
    public Guid IdentityUserId { get; set; }
    public Guid RequestorGuid { get; set; }

    public UserRemoveEvent(Guid identityUserId, Guid requestorGuid)
    {
        IdentityUserId = identityUserId;
        RequestorGuid = requestorGuid;
    }
}