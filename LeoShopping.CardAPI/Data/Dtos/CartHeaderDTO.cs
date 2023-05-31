using LeoShopping.CartAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeoShopping.CartAPI.Model
{
    public class CartHeaderDTO
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
    }
}
