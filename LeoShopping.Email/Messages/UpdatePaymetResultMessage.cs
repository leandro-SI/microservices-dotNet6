namespace LeoShopping.Email.Messages
{
    public class UpdatePaymetResultMessage
    {
        public long OrderId { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }
    }
}
