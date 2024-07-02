using AutoMapper;
using E.API.Contracts.Carts.Requests;
using E.API.Contracts.Carts.Responses;
using E.Application.Carts.Commands;
using E.Domain.Entities.Carts;

namespace E.API.MappingProfiles;

public class CartMapping : Profile
{
    public CartMapping()
    {
        CreateMap<CartDetails, CartResponse>();
        CreateMap<CartItemAdd, CartItemAddCommand>();
        CreateMap<CartItemUpdate, CartItemUpdateCommand>();
    }
}