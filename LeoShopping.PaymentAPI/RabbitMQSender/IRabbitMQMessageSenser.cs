using LeoShopping.MessageBus;

namespace LeoShopping.PaymentAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSenser
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
