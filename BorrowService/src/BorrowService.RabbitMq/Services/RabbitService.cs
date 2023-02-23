using BorrowService.RabbitMq.Options;
using BorrowService.RabbitMq.Services.Abstract;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace BorrowService.RabbitMq.Services
{
    public class RabbitService : IRabbitService
    {
        private readonly RabbitOptions _options;

        public RabbitService(IOptions<RabbitOptions> options)
        {
            _options = options.Value;
        }

        public async Task ExecuteAsync(string message)
        {
            await Task.Run(() =>
            {

                var factory = new ConnectionFactory()
                {
                    HostName = _options.HostName,
                    Port = _options.Port,
                    UserName = _options.UserName,
                    Password = _options.Password
                };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: _options.Queue,
                                             durable: false,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);

                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: string.Empty,
                                             routingKey: _options.Queue,
                                             basicProperties: null,
                                             body: body);
                    }
                }
            });
        }

        public async Task LockAsync(int bookId)
        {
            var rabbitMessage = new RabbitMessage()
            {
                Id = bookId,
                Action = Enums.Action.Lock,
            };

            var message = JsonConvert.SerializeObject(rabbitMessage);

            await ExecuteAsync(message);
        }

        public async Task UnlockAsync(int bookId)
        {
            var rabbitMessage = new RabbitMessage()
            {
                Id = bookId,
                Action = Enums.Action.Lock,
            };

            var message = JsonConvert.SerializeObject(rabbitMessage);

            await ExecuteAsync(message);
        }
    }
}
