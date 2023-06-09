using LeoShopping.OrderAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeoShopping.OrderAPI.Model
{
    [Table("order_detail")]
    public class OrderDetail : BaseEntity
    {
        public long OrderHeaderId { get; set; }

        [ForeignKey(nameof(OrderHeaderId))]
        public virtual OrderHeader OrderHeader { get; set; }

        [Column("product_id")]
        public long ProductId { get; set; }

        [Column("count")]
        public int Count { get; set; }

        [Column("product_name")]
        public string ProductName { get; set; }

        [Column("price")]
        public decimal Price { get; set; }
    }
}
