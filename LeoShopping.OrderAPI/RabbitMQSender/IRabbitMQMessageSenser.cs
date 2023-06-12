using LeoShopping.MessageBus;

namespace LeoShopping.OrderAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSenser
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
