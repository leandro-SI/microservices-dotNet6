using LeoShopping.CartAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeoShopping.CartAPI.Model
{
    [Table("cart_detail")]
    public class CartDetail : BaseEntity
    {
        public long CartHeaderId { get; set; }

        [ForeignKey(nameof(CartHeaderId))]
        public virtual CartHeader CartHeader { get; set; }

        public long ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        [Column("count")]
        public int Count { get; set; }
    }
}
