using BorrowService.Borrowings.Services.Abstract;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace BorrowService.Borrowings.Services
{
    public class HangfireService : IHangfireService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMailService _mailService;

        public HangfireService(ApplicationContext applicationContext,
            IMailService mailService)
        {
            _applicationContext = applicationContext;
            _mailService = mailService;
        }

        public void Run()
        {
            RecurringJob.AddOrUpdate("mailJob", () => SetJobs(), Cron.Minutely);
        }

        public void SetJobs()
        {
            var borrowings = _applicationContext.Borrowings.AsNoTracking().Where(x => x.ExpirationDate == DateTime.Now.Date + TimeSpan.FromDays(3)).ToList();

            if (borrowings != null)
            {
                foreach (var borrowing in borrowings)
                {
                    BackgroundJob.Enqueue(() => _mailService.SendMessageAsync(borrowing.UserEmail, borrowing.BookTitle));
                }
            }
        }
    }
}
