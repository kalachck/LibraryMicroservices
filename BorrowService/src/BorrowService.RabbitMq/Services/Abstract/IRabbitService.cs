namespace BorrowService.RabbitMq.Services.Abstract
{
    public interface IRabbitService
    {
        Task LockAsync(int bookId);

        Task UnlockAsync(int bookId);

        Task ExecuteAsync(string message);
    }
}
