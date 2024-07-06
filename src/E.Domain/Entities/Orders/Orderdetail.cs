using E.Domain.Entities.Orders.OrderValidators;
using E.Domain.Entities.Products;
using E.Domain.Exceptions;

namespace E.Domain.Entities.Orders;

public class Orderdetail : BaseEntity
{
    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime? ShipDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public Order? Order { get; set; }
    public Product? Product { get; set; }
    private readonly OrderDetailsValidator _validator;

    public Orderdetail()
    {
        _validator = new OrderDetailsValidator();
    }
    private void ValidateAndThrow()
    {
        var validationResult = _validator.Validate(this);
        if (!validationResult.IsValid)
        {
            var exception = new OrderDetailsInvalidException($"{validationResult}");
            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
            }
            throw exception;
        }
    }

    public static Orderdetail AddOrderDetail(Guid orderId, Guid productId,
        int quantity)
    {
        var objectToValidate = new Orderdetail
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            ProductId = productId,
            Quantity = quantity
        };
        objectToValidate.ValidateAndThrow();
        return objectToValidate;
    }
}