using LeoShopping.CartAPI.Data.Dtos;

namespace LeoShopping.CartAPI.Repository.Interfaces
{
    public interface ICouponReposiroty
    {
        Task<CouponDTO> GetCouponByCouponCode(string couponCode, string token);
    }
}
