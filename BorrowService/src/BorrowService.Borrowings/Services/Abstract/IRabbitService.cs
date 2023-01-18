namespace BorrowService.Borrowings.Services.Abstract
{
    public interface IRabbitService
    {
        void SendMessage(string message);
    }
}
