using E.Application.Carts.Commands;
using E.Application.Carts.Events;
using E.Application.Enums;
using E.Application.Identity;
using E.Application.Models;
using E.Application.Products;
using E.Application.Services.CartServices;
using E.DAL.EventPublishers;
using E.DAL.UoW;
using MediatR;

namespace E.Application.Carts.CommandHandlers;

public class CartItemRemoveCommandHandler :
    IRequestHandler<CartItemRemoveCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly CartService _cartService;

    public CartItemRemoveCommandHandler(IUnitOfWork unitOfWork,
        IEventPublisher eventPublisher, CartService cartService)
    {
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _cartService = cartService;
    }

    public async Task<OperationResult<bool>> Handle(CartItemRemoveCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<bool>();
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
            if (user is null)
            {
                result.AddError(ErrorCode.NotFound,
                    UserErrorMessage.UserNotFound(request.UserId));
                return result;
            }

            var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
            if (product is null)
            {
                result.AddError(ErrorCode.NotFound,
                    ProductErrorMessage.ProductNotFound(request.ProductId));
                return result;
            }
            var cart = await _unitOfWork.Carts.FirstOrDefaultAsync(cd =>
            cd.UserId == request.UserId && cd.ProductId == request.ProductId);

            if (cart is null)
            {
                result.AddError(ErrorCode.NotFound,
                    CartErrorMessage.CartNotFound);
            }
            _cartService.DeleteProductInCart(cart,product.Id);
            _unitOfWork.Carts.Remove(cart);

            var cartDeleteEvent = new CartItemRemoveEvent(cart.Id);
            await _eventPublisher.PublishAsync(cartDeleteEvent);

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