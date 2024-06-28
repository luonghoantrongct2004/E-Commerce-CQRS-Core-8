using E.Application.Enums;
using E.Application.Identity.Commands;
using E.Application.Identity.EventHandlers;
using E.Application.Models;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E.Application.Identity.CommandHandlers;

public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;

    public RemoveUserCommandHandler(IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
    }

    public async Task<OperationResult<bool>> Handle(RemoveUserCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<bool>();
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var userProfile = await _unitOfWork.Users.
                FirstOrDefaultAsync(up => up.Id == request.IdentityUserId);

            if (userProfile is null)
            {
                result.AddError(ErrorCode.NotFound, 
                    IdentityErrorMessages.NonExistentIdentityUser);
                return result;
            }
            _unitOfWork.Users.Remove(userProfile);

            var userEvent = new UserRemoveEvent(request.IdentityUserId);
            await _eventPublisher.PublishAsync(userEvent);

            await _unitOfWork.CommitAsync();

            result.Payload = true;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            result.AddUnknownError(e.Message);
        }

        return result;
    }
}