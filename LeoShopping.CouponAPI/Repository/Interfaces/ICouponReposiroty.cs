using LeoShopping.CouponAPI.Data.Dtos;

namespace LeoShopping.CouponAPI.Repository.Interfaces
{
    public interface ICouponReposiroty
    {
        Task<CouponDTO> GetCouponByCouponCode(string couponCode);
    }
}
