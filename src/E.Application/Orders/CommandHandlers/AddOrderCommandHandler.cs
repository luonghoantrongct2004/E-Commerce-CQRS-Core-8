using E.Application.Enums;
using E.Application.Identity;
using E.Application.Models;
using E.Application.Orders.Commands;
using E.Application.Orders.Events;
using E.Application.Services.OrderServices;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using E.Domain.Entities.Orders;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace E.Application.Orders.CommandHandlers;

public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand,
    OperationResult<Order>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly OrderService _orderService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AddOrderCommandHandler(IUnitOfWork unitOfWork,
        IEventPublisher eventPublisher, OrderService orderService, 
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _orderService = orderService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResult<Order>> Handle(AddOrderCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<Order>();
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("IdentityId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                result.AddError(ErrorCode.NotFound,
                    UserErrorMessage.TokenNotFound);
                return result;
            }

            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                result.AddUnknownError("Invalid format in the token.");
                return result;
            }

            var order = _orderService.AddOrder(request.OrderDetails
                ,userId ,request.TotalPrice
                ,request.Note, request.PaymentMethod,
                request.ContactPhone);

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CompleteAsync();

            var orderAddEvent = new AddOrderEvent(order.Id, order.UserId,
                order.OrderDate, order.Note, order.PaymentMethod, order.PaymentDate,
                order.ContactPhone, order.TotalPrice,order.StatusString, order.OrderDetails);
            await _eventPublisher.PublishAsync(orderAddEvent);

            await _unitOfWork.CommitAsync();

            result.Payload = order;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            result.AddUnknownError(e.Message);
        }

        return result;
    }
}