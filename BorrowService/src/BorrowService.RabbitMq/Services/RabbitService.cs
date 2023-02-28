using BorrowService.RabbitMq.Options;
using BorrowService.RabbitMq.Services.Abstract;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace BorrowService.RabbitMq.Services
{
    public class RabbitService : IRabbitService
    {
        private readonly RabbitOptions _options;

        private IConnectionFactory _factory;

        public RabbitService(IOptions<RabbitOptions> options)
        {
            _options = options.Value;

            _factory = new ConnectionFactory()
            {
                HostName = _options.HostName,
                Port = _options.Port,
                UserName = _options.UserName,
                Password = _options.Password,
            };
        }

        public async Task LockAsync(int bookId)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: _options.LockExchange, type: ExchangeType.Direct);

            channel.QueueDeclare(queue: _options.LockQueue,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var body = Encoding.UTF8.GetBytes(bookId.ToString());

            channel.BasicPublish(exchange: _options.LockExchange,
                                    routingKey: _options.LockQueue,
                                    basicProperties: null,
                                    body: body);
        }

        public async Task UnlockAsync(int bookId)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: _options.UnlockExchange, type: ExchangeType.Direct);

            channel.QueueDeclare(queue: _options.UnlockQueue,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var body = Encoding.UTF8.GetBytes(bookId.ToString());

            channel.BasicPublish(exchange: _options.UnlockExchange,
                                    routingKey: _options.UnlockQueue,
                                    basicProperties: null,
                                    body: body);

        }
    }
}
