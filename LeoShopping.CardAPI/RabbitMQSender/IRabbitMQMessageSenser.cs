using LeoShopping.MessageBus;

namespace LeoShopping.CartAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSenser
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
