using E.Domain.Entities.Orders;
using E.Domain.Entities.Products;
using E.Domain.Enum;

namespace E.Application.Services.OrderServices;

public class OrderService
{
    private readonly OrderValidationService _validationService;
    private readonly OrderProcessingService _processingService;

    public OrderService(OrderValidationService validationService, OrderProcessingService processingService)
    {
        _validationService = validationService;
        _processingService = processingService;
    }
    public Order AddOrder(Guid userId, decimal totalPrice,
        DateTime orderDate, OrderStatus status, List<Orderdetail> orderDetails,
        string contactPhone, string note, string paymentMethod)
    {
        var objectToValidate = new Order
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            TotalPrice = totalPrice,
            OrderDate = orderDate,
            Status = status,
            OrderDetails = orderDetails,
            ContactPhone = contactPhone,
            Note = note,
            PaymentDate = DateTime.Now,
            PaymentMethod = paymentMethod
        };
        _validationService.ValidateAndThrow(objectToValidate);
        return objectToValidate;
    }
    public void ConfirmOrder(Order order, Product product)
    {
        _processingService.ConfirmPurchar(order, product);
    }

    public void CancelOrder(Order order, Product product)
    {
        _processingService.CancelPurchar(order, product);
    }
}