using E.API.Contracts.Coupons.Requests;
using E.API.Contracts.Coupons.Responses;

namespace E.API.MappingProfiles;

public class CouponMapping : Profile
{
    public CouponMapping()
    {
        CreateMap<Coupon, CouponResponse>();
        CreateMap<ApplyCoupon, ApplyCouponCommand>();
        CreateMap<CreateCoupon, CreateCouponCommand>();
        CreateMap<DisableCoupon, DisableCouponCommand>();
        CreateMap<UpdateCoupon, UpdateCouponCommand>();
    }
}
