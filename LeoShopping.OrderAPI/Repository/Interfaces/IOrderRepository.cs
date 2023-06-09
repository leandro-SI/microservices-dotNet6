using LeoShopping.OrderAPI.Model;

namespace LeoShopping.OrderAPI.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(OrderHeader header);
        Task UpdateOrderPaymentStatus(long orderHeaderId, bool paid);

    }
}
