using AutoMapper;
using E.Application.Enums;
using E.Application.Identity.Commands;
using E.Application.Identity.Events;
using E.Application.Models;
using E.Application.Services.UserServices;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Users;
using E.Domain.Entities.Users.Dto;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E.Application.Identity.CommandHandlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, OperationResult<IdentityUserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly AppDbContext _appDbContext;
    private readonly UserManager<DomainUser> _userManager;
    private readonly IMapper _mapper;
    private OperationResult<IdentityUserDto> _result = new();
    private readonly UserService _userService;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IEventPublisher eventPublisher,
        AppDbContext appDbContext, UserManager<DomainUser> userManager, IMapper mapper, UserService userService)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _appDbContext = appDbContext;
        _userManager = userManager;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<OperationResult<IdentityUserDto>> Handle(UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var identityUser = await _appDbContext.Users.
                FirstOrDefaultAsync(u => u.Id == request.UserId);
            if (identityUser is null)
            {
                _result.AddError(ErrorCode.IdentityUserDoesNotExist,
                    IdentityErrorMessages.NonExistentIdentityUser);
                return _result;
            }
            _userService.UpdateBasicInfo(
                identityUser,
                username: request.Username,
                password: request.Password,
                fullName: request.FullName,
                avatar: request.Avatar,
                address: request.Address,
                currentCity: request.CurrentCity
            );
            _unitOfWork.Users.Update(identityUser);

            var userEvent = new UserUpdateEvent(identityUser.Id, request.Username, request.Password,
                request.FullName, request.CreatedDate, request.Avatar, request.Address, request.CurrentCity);
            await _eventPublisher.PublishAsync(userEvent);

            await _unitOfWork.CommitAsync();

            _result.Payload = _mapper.Map<IdentityUserDto>(identityUser);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            _result.AddUnknownError(ex.Message);
        }
        return _result;
    }
}