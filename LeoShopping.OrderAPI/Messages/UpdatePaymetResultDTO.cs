namespace LeoShopping.OrderAPI.Messages
{
    public class UpdatePaymetResultDTO
    {
        public long OrderId { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }
    }
}
