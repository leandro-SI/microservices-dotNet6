using LeoShopping.CartAPI.Model;

namespace LeoShopping.CartAPI.Repository.Interfaces
{
    public interface ICartRepository
    {
        Task<CartDTO> FindCartbyUserId(string userId);
        Task<CartDTO> SaveOrUpdateCart(CartDTO cart);
        Task<bool> RemoveFromCart(long cartDetailsId);
        Task<bool> ApplyCoupon(string userId, string couponCode);
        Task<bool> RemoveCoupon(string userId);
        Task<bool> ClearCart(string userId);


    }
}
