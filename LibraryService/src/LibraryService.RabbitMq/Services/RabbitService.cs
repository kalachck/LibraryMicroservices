using LibraryService.BussinesLogic.Services.Abstract;
using LibraryService.RabbitMq.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

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

                _channel.ExchangeDeclare(exchange: _options.LockExchange, type: ExchangeType.Direct);
                _channel.ExchangeDeclare(exchange: _options.UnlockExchange, type: ExchangeType.Direct);

                _channel.QueueDeclare(queue: _options.LockQueue,
                                      exclusive: false,
                                      autoDelete: false);

                _channel.QueueDeclare(queue: _options.UnlockQueue,
                                      exclusive: false,
                                      autoDelete: false);

                _channel.QueueBind(queue: _options.LockQueue,
                                   exchange: _options.LockExchange,
                                   routingKey: string.Empty);

                _channel.QueueBind(queue: _options.UnlockQueue,
                                   exchange: _options.UnlockExchange,
                                   routingKey: string.Empty);

                var lockConsumer = new EventingBasicConsumer(_channel);

                lockConsumer.Received += (sender, args) =>
                {
                    var body = args.Body.ToArray();

                    var message = Encoding.UTF8.GetString(body);

                    _bookService.LockAsync(message);
                };

                var unlockConsumer = new EventingBasicConsumer(_channel);

                unlockConsumer.Received += (sender, args) =>
                {
                    var body = args.Body.ToArray();

                    var message = Encoding.UTF8.GetString(body);

                    _bookService.UnlockAsync(message);
                };

                _channel.BasicConsume(queue: _options.LockQueue,
                                      autoAck: false,
                                      consumer: lockConsumer);

                _channel.BasicConsume(queue: _options.UnlockQueue,
                                      autoAck: false,
                                      consumer: unlockConsumer);
            });
        }

        public override void Dispose()
        {
            _channel.Dispose();

            base.Dispose();
        }
    }
}
