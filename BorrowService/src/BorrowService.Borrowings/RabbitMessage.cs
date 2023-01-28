using BorrowService.Borrowings.Enums;

namespace BorrowService.Borrowings
{
    public class RabbitMessage
    {
        public Topic Topic { get; set; }

        public int BookId { get; set; }
    }
}
