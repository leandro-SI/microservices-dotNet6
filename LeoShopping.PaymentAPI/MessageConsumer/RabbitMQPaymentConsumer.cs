using LeoShopping.PaymentAPI.Messages;
using LeoShopping.PaymentAPI.RabbitMQSender;
using LeoShopping.PaymentProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace LeoShopping.PaymentAPI.MessageConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private IRabbitMQMessageSenser _rabbitMQMessageSenser;
        private readonly IProcessPayment _processPayment;

        public RabbitMQPaymentConsumer(IProcessPayment processPayment, IRabbitMQMessageSenser rabbitMQMessageSenser)
        {
            _processPayment = processPayment;
            _rabbitMQMessageSenser = rabbitMQMessageSenser;

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "orderpaymentprocess", false, false, false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (channel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                PaymentMessage dto = JsonSerializer.Deserialize<PaymentMessage>(content);
                ProcessPayment(dto).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };

            _channel.BasicConsume("orderpaymentprocess", false, consumer);

            return Task.CompletedTask;

        }

        private async Task ProcessPayment(PaymentMessage dto)
        {
            var result = _processPayment.PaymentProcessor();

            UpdatePaymetResultMessage paymentResult = new UpdatePaymetResultMessage()
            {
                Status = result,
                OrderId = dto.OrderId,
                Email = dto.Email
            };

            try
            {
                _rabbitMQMessageSenser.SendMessage(paymentResult, "orderpaymentresultqueue");
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
