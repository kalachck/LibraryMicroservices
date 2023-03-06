using BorrowService.Borrowings.Repositories.Abstract;
using BorrowService.Hangfire.Services.Abstract;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BorrowService.Hangfire.Services
{
    public class HangfireService : BackgroundService
    {
        private readonly IBorrowingRepository _repository;
        private readonly IMailService _mailService;

        public HangfireService(IServiceScopeFactory factory)
        {
            _repository = factory.CreateScope().ServiceProvider.GetRequiredService<IBorrowingRepository>();
            _mailService = factory.CreateScope().ServiceProvider.GetRequiredService<IMailService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() =>
            {
                RecurringJob.AddOrUpdate("MailJob", () => SetJobs(), Cron.Daily);
            });
        }

        public async Task SetJobs()
        {
            var borrowings = await _repository.GetBorrowingsAsync();

            if (borrowings.Count != 0)
            {
                foreach (var borrowing in borrowings)
                {
                    BackgroundJob.Enqueue(() => _mailService.SendMessageAsync(borrowing.UserEmail, borrowing.BookTitle));
                }
            }
        }
    }
}
