using E.Domain.Entities.Orders;

namespace E.Application.Services.OrderServices;

public class OrderDetailService
{
    private readonly OrderDetailValidationService _validationService;

    public OrderDetailService(OrderDetailValidationService validationService)
    {
        _validationService = validationService;
    }

    public OrderDetail AddOrderDetail(Guid orderId, Guid productId,
        int quantity)
    {
        var objectToValidate = new OrderDetail
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            ProductId = productId,
            Quantity = quantity
        };
        _validationService.ValidateAndThrow(objectToValidate);
        return objectToValidate;
    }
}