using LeoShopping.MessageBus;

namespace LeoShopping.PaymentAPI.Messages
{
    public class UpdatePaymetResultMessage : BaseMessage
    {
        public long OrderId { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }
    }
}
