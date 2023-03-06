namespace BorrowService.Borrowings.Exceptions
{
    public class NotAvailableException : Exception
    {
        public NotAvailableException(string? message) : base(message)
        { }
    }
}
