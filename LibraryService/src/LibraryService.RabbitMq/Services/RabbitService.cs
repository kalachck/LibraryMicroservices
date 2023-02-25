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
        private IModel _channel;


        public RabbitService(IOptions<RabbitOptions> options,
            IServiceScopeFactory factory)
        {
            _options = options.Value;
            _bookService = factory.CreateScope().ServiceProvider.GetRequiredService<IBookService>();
        }

        private async Task<IModel> CreateModel()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _options.HostName,
                Port = _options.Port,
                UserName = _options.UserName,
                Password = _options.Password
            };

            var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            return await Task.FromResult(channel);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(async () =>
            {
                _channel = await CreateModel();

                _channel.QueueDeclare(queue: _options.Queue,
                                exclusive: false,
                                autoDelete: false);

                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += (sender, args) =>
                {
                    var body = args.Body.ToArray();

                    var message = Encoding.UTF8.GetString(body);

                    _bookService.ChangeStatus(message);
                };

                _channel.BasicConsume(queue: _options.Queue,
                                        autoAck: false,
                                        consumer: consumer);
            });
        }

        public override void Dispose()
        {
            _channel.Dispose();

            base.Dispose();
        }
    }
}
