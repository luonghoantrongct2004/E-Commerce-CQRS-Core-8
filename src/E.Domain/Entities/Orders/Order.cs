using E.Domain.Entities.Orders.OrderValidations;
using E.Domain.Entities.Products;
using E.Domain.Entities.Users;
using E.Domain.Enum;
using E.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E.Domain.Entities.Orders;

public class Order : BaseEntity
{
    public Guid UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime? ShipDate { get; set; }

    public DateTime PaymentDate { get; set; }

    public string? Note { get; set; }

    public Guid? ShipperId { get; set; }
    public DomainUser User { get; set; }
    public string PaymentMethod { get; set; }
    public string ContactPhone { get; set; }

    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public decimal TotalPrice { get; set; }

    public ICollection<Orderdetail> OrderDetails { get; set; }
    /*public virtual Shipper Shipper { get; set; }*/

    [NotMapped]
    public OrderStatus Status { get; set; }

    [Column("Status")]
    public string StatusString
    {
        get => Status.ToString();
        set => Status = System.Enum.TryParse(value, out OrderStatus status)
            ? status : OrderStatus.Pending;
    }

    private readonly OrderValidator _validator;

    public Order()
    {
        _validator = new OrderValidator();
    }
    private void ValidateAndThrow()
    {
        var validationResult = _validator.Validate(this);
        if (!validationResult.IsValid)
        {
            var exception = new OrderInvalidException($"{validationResult}");
            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add($"Field {error.PropertyName}: {error.ErrorMessage}");
            }
            throw exception;
        }
    }

    public static Order AddOrder(Guid userId, decimal totalPrice,
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
        objectToValidate.ValidateAndThrow();
        return objectToValidate;
    }
    public void ConfirmPurchar(Product product)
    {
        if(Status == OrderStatus.Confirmed)
        {
            var orderDetail = OrderDetails.FirstOrDefault();
            if(orderDetail != null)
            {
                product.SoldQuantity -= orderDetail.Quantity;
                Status = OrderStatus.Delivered;
            }
        }
    }
    public void CancelPurchar(Product product)
    {
        if(Status == OrderStatus.Confirmed)
        {
            var orderDetail = OrderDetails.FirstOrDefault();
            if(orderDetail != null)
            {
                product.SoldQuantity += orderDetail.Quantity;
                Status = OrderStatus.Canceled;
            }
        }
    }
}