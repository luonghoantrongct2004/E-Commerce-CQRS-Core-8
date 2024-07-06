using E.Application.Enums;
using E.Application.Identity;
using E.Application.Models;
using E.Application.Orders.Commands;
using E.Application.Orders.Events;
using E.Application.Products;
using E.Application.Services.OrderServices;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace E.Application.Orders.CommandHandlers;

public class CancelOrderCommandHandler :
    IRequestHandler<CancelOrderCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly OrderService _orderService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CancelOrderCommandHandler(IUnitOfWork unitOfWork,
        IEventPublisher eventPublisher, OrderService orderService,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _orderService = orderService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResult<bool>> Handle(CancelOrderCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<bool>();
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
            var order = await _unitOfWork.Orders.FirstOrDefaultAsync(
                o => o.UserId == userId && o.Id == request.Id);
            if (order == null)
            {
                result.AddError(ErrorCode.NotFound, 
                    OrderErrorMessage.OrderNotFound);
                return result;
            }

            var orderDetail = order.OrderDetails.FirstOrDefault();
            if (orderDetail == null)
            {
                result.AddError(ErrorCode.NotFound,
                    OrderErrorMessage.OrderNotFound);
                return result;
            }

            var productId = orderDetail.ProductId;
            var product = await _unitOfWork.Products.FirstOrDefaultAsync(
                p => p.Id == productId);
            if (product == null)
            {
                result.AddError(ErrorCode.NotFound,
                    ProductErrorMessage.ProductNotFound(request.Product.Id));
                return result;
            }
            _orderService.CancelOrder(order, product);
             _unitOfWork.Orders.Update(order);
            _unitOfWork.Products.Update(product);

            await _unitOfWork.CompleteAsync();

            var orderAddEvent = new CancelOrderEvent(order, product);
            await _eventPublisher.PublishAsync(orderAddEvent);

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