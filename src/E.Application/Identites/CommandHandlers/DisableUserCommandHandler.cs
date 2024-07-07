using E.Application.Enums;
using E.Application.Identity.Commands;
using E.Application.Identity.EventHandlers;
using E.Application.Models;
using E.Application.Services.UserServices;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using MediatR;

namespace E.Application.Identity.CommandHandlers;

public class DisableUserCommandHandler : IRequestHandler<DisableUserCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly UserService _userService;

    public DisableUserCommandHandler(IUnitOfWork unitOfWork,
        IEventPublisher eventPublisher, UserService userService)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _userService = userService;
    }

    public async Task<OperationResult<bool>> Handle(DisableUserCommand request,
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
            _userService.DisableUser(userProfile);
            _unitOfWork.Users.Update(userProfile);

            var userEvent = new UserDisableEvent(request.IdentityUserId);
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