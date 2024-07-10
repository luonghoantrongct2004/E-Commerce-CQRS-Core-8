using E.API.Contracts.Orders.Requests;
using E.API.Contracts.Orders.Responses;

namespace E.API.MappingProfiles;

public class OrderMapping : Profile
{
    public OrderMapping()
    {
        CreateMap<Order, OrderResponse>();
        CreateMap<AddOrder, AddOrderCommand>()
            .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));
        CreateMap<CancelOrder, CancelOrderCommand>();
        CreateMap<ConfirmOrder, ConfirmOrderCommand>();
    }
}