using LeoShopping.OrderAPI.Messages;
using LeoShopping.OrderAPI.Model;
using LeoShopping.OrderAPI.RabbitMQSender;
using LeoShopping.OrderAPI.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace LeoShopping.OrderAPI.RabbitMQConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private readonly OrderRepository _repository;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQPaymentConsumer(OrderRepository repository)
        {
            _repository = repository;

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "orderpaymentresultqueue", false, false, false, arguments: null);            
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (channel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                UpdatePaymetResultDTO dto = JsonSerializer.Deserialize<UpdatePaymetResultDTO>(content);
                UpdatePaymentStatus(dto).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };

            _channel.BasicConsume("orderpaymentresultqueue", false, consumer);

            return Task.CompletedTask;

        }

        private async Task UpdatePaymentStatus(UpdatePaymetResultDTO dto)
        {

            try
            {
                await _repository.UpdateOrderPaymentStatus(dto.OrderId, dto.Status);

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
