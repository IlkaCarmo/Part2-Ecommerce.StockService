using Ecommerce.StockService.DTOs;
using Ecommerce.StockService.Services;
using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Ecommerce.StockService.Messaging
{
    public class RabbitMqConsumer
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "order_created";
        private readonly string _username = "guest";
        private readonly string _password = "guest";
        private readonly ProductServer _productService;

        public RabbitMqConsumer(ProductServer productService)
        {
            _productService = productService;
        }

        public void Start()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    var orderEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(message);
                    if (orderEvent != null)
                    {
                        Console.WriteLine($"[x] Received order {orderEvent.OrderId}");
                        await _productService.ProcessOrderAsync(orderEvent);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[!] Error processing message: {ex.Message}");
                }
            };

            channel.BasicConsume(
                queue: _queueName,
                autoAck: true,
                consumer: consumer
            );

            Console.WriteLine("[*] Waiting for messages. Press [enter] to exit.");
            Console.ReadLine();
        }
    }

}
