namespace BorrowService.Api.RequestModels
{
    public class BorrowingRequestModel
    {
        public string UserEmail { get; set; }

        public int BookId { get; set; }

        public DateTime AddingDate { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
