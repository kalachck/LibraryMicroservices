namespace BorrowService.Borrowings.Services.Abstract
{
    public interface IDbSaver
    {
        Task SaveChangesAsync();
    }
}
