using E.Domain.Entities;
using E.Domain.Entities.Orders;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E.Application.Orders.Events;

public class AddOrderEvent : BaseEntity, INotification
{
    public Guid UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public string? Note { get; set; }
    public string PaymentMethod { get; set; }
    public string ContactPhone { get; set; }

    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public decimal TotalPrice { get; set; }
    public ICollection<Orderdetail> OrderDetails { get; set; }

    public AddOrderEvent(Guid id ,Guid userId, DateTime orderDate, string? note,
        string paymentMethod, string contactPhone, decimal totalPrice,
        ICollection<Orderdetail> orderDetails)
    {
        Id = id;
        UserId = userId;
        OrderDate = orderDate;
        Note = note;
        PaymentMethod = paymentMethod;
        ContactPhone = contactPhone;
        TotalPrice = totalPrice;
        OrderDetails = orderDetails;
    }
}