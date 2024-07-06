using E.Domain.Entities.Orders;
using E.Domain.Entities.Products;
using E.Domain.Enum;

namespace E.Application.Services.OrderServices;

public class OrderProcessingService
{
    public void ConfirmPurchar(Order order, Product product)
    {
        if (order.Status == OrderStatus.Confirmed)
        {
            var orderDetail = order.OrderDetails.FirstOrDefault();
            if (orderDetail != null)
            {
                product.SoldQuantity -= orderDetail.Quantity;
                order.Status = OrderStatus.Delivered;
            }
        }
    }

    public void CancelPurchar(Order order, Product product)
    {
        if (order.Status == OrderStatus.Confirmed)
        {
            var orderDetail = order.OrderDetails.FirstOrDefault();
            if (orderDetail != null)
            {
                product.SoldQuantity += orderDetail.Quantity;
                order.Status = OrderStatus.Canceled;
            }
        }
    }
}