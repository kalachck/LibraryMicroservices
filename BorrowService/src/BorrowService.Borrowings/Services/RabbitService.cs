using BorrowService.Borrowings.Options;
using BorrowService.Borrowings.Services.Abstract;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace BorrowService.Borrowings.Services
{
    public class RabbitService : IRabbitService
    {
        private readonly RabbitOptions _options;

        public RabbitService(IOptions<RabbitOptions> options)
        {
            _options = options.Value;
        }

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _options.HostName,
                UserName = _options.UserName,
                Password = _options.Password
            };

            using var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _options.Queue, exclusive: false, autoDelete: false);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(string.Empty, _options.Queue, null, body);

            channel.Close();
            connection.Close();
        }
    }
}
