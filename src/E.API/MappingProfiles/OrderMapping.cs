using E.API.Contracts.Orders.Requests;
using E.API.Contracts.Orders.Responses;

using E.Domain.Entities.Orders;

namespace E.API.MappingProfiles;

public class OrderMapping : Profile
{
    public OrderMapping()
    {
        CreateMap<Order, OrderResponse>();
        CreateMap<AddOrder, AddOrderCommand>();
        CreateMap<CancelOrder, CancelOrderCommand>();
        CreateMap<ConfirmOrder, ConfirmOrderCommand>();
    }
}
