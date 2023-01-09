namespace BorrowService.Borrowings.Entities
{
    public class BorrowingEntity
    {
        public int Id { get; set; }

        public string UserEmail { get; set; }

        public int BookId { get; set; }

        public DateTime AddingDate { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
