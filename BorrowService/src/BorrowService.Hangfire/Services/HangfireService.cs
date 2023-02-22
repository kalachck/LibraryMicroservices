using BorrowService.Borrowings.Repositories.Abstract;
using BorrowService.Hangfire.Services.Abstract;
using Hangfire;
using Microsoft.Extensions.Hosting;

namespace BorrowService.Hangfire.Services
{
    public class HangfireService : BackgroundService
    {
        private readonly IBorrowingRepository _repository;
        private readonly IMailService _mailService;

        public HangfireService(IBorrowingRepository repository,
            IMailService mailService)
        {
            _repository = repository;
            _mailService = mailService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            RecurringJob.AddOrUpdate("MailJob", () => SetJobs(), Cron.Daily);

            return Task.CompletedTask;
        }

        private async Task SetJobs()
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
