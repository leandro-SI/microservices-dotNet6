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
    public class RabbitMQCheckoutConsumer : BackgroundService
    {
        private readonly OrderRepository _repository;
        private IConnection _connection;
        private IModel _channel;
        private IRabbitMQMessageSenser _rabbitMQMessageSenser;

        public RabbitMQCheckoutConsumer(OrderRepository repository, IRabbitMQMessageSenser rabbitMQMessageSenser)
        {
            _repository = repository;
            _rabbitMQMessageSenser = rabbitMQMessageSenser;

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "checkoutqueue", false, false, false, arguments: null);            
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (channel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                CheckoutHeaderDTO dto = JsonSerializer.Deserialize<CheckoutHeaderDTO>(content);
                ProcessOrder(dto).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };

            _channel.BasicConsume("checkoutqueue", false, consumer);

            return Task.CompletedTask;

        }

        private async Task ProcessOrder(CheckoutHeaderDTO dto)
        {
            OrderHeader order = new OrderHeader()
            {
                UserId = dto.UserId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                OrderDetails = new List<OrderDetail>(),
                CardNumber = dto.CardNumber,
                CouponCode = dto.CouponCode,
                CVV = dto.CVV,
                DiscountTotal = dto.DiscountTotal,
                Email = dto.Email,
                ExpiryMonthYear = dto.ExpiryMonthYear,
                OrderTime = DateTime.Now,
                PaymentStatus = false,
                Phone = dto.Phone,
                PurchaseAmount = dto.PurchaseAmount
            };

            foreach (var item in dto.CartDetails)
            {
                OrderDetail detail = new OrderDetail()
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Price = item.Product.Price,
                    Count = item.Count
                };

                order.CartTotalItems += item.Count;
                order.OrderDetails.Add(detail);
            }

            await _repository.AddOrder(order);

            PaymentDTO payment = new PaymentDTO()
            {
                Name = order.FirstName + " " + order.LastName,
                CardNumber = order.CardNumber,
                CVV = order.CVV,
                ExpiryMonthYear = order.ExpiryMonthYear,
                OrderId = order.Id,
                PurchageAmount = order.PurchaseAmount,
                Email = order.Email
            };

            try
            {
                _rabbitMQMessageSenser.SendMessage(payment, "orderpaymentprocess");
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
