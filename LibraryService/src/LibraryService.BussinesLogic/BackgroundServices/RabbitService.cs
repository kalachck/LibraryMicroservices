using LibrarySevice.BussinesLogic.Options;
using LibrarySevice.BussinesLogic.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace LibrarySevice.BussinesLogic.BackgroundServices
{
    public class RabbitService : BackgroundService
    {
        private readonly RabbitOptions _options;
        private readonly IBookService _bookService;
        private IConnection _connection;
        private IModel _channel;

        public RabbitService(IOptions<RabbitOptions> options,
           IServiceScopeFactory factory)
        {
            _options = options.Value;
            _bookService = factory.CreateScope().ServiceProvider.GetRequiredService<IBookService>();

            Configure();
        }

        public void Configure()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _options.HostName,
                Port = _options.Port,
                UserName = _options.UserName,
                Password = _options.Password,
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _options.Queue, exclusive: false, autoDelete: false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, args) =>
            {
                var message = Encoding.UTF8.GetString(args.Body.ToArray());

                _bookService.ChangeStatus(message);
            };

            _channel.BasicConsume(_options.Queue, true, consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();

            base.Dispose();
        }
    }
}
