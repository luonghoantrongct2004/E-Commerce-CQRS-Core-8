using E.API.Contracts.Carts.Requests;
using E.API.Contracts.Carts.Responses;

namespace E.API.MappingProfiles;

public class CartMapping : Profile
{
    public CartMapping()
    {
        CreateMap<CartDetails, CartResponse>();
        CreateMap<CartItemAdd, CartItemAddCommand>();
    }
}