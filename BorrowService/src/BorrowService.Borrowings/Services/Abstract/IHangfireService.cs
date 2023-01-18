namespace BorrowService.Borrowings.Services.Abstract
{
    public interface IHangfireService
    {
        void Run();

        void SetJobs();
    }
}
