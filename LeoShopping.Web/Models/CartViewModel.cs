namespace LeoShopping.Web.Models
{
    public class CartViewModel
    {
        public CartHeaderViewModel CartHeader { get; set; }
        public IEnumerable<CartDetailViewModel> CartDetail { get; set; }
    }
}
