using LibraryService.BussinesLogic.Services.Abstract;
using LibraryService.RabbitMq.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace LibraryService.RabbitMq.Services
{
    public class RabbitService : BackgroundService
    {
        private readonly RabbitOptions _options;
        private readonly IBookService _bookService;

        public RabbitService(IOptions<RabbitOptions> options,
            IServiceScopeFactory factory)
        {
            _options = options.Value;
            _bookService = factory.CreateScope().ServiceProvider.GetRequiredService<IBookService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _options.HostName,
                    Port = _options.Port,
                    UserName = _options.UserName,
                    Password = _options.Password,
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

                        var consumer = new EventingBasicConsumer(channel);

                        consumer.Received += (sender, args) =>
                        {
                            var body = args.Body.ToArray();

                            var message = Encoding.UTF8.GetString(body);

                            _bookService.ChangeStatus(message);
                        };
                    }
                }
            });
        }

    }
}
