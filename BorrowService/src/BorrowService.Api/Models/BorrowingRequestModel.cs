using BorrowService.Borrowings.Validators;

namespace BorrowService.Api.Models
{
    public class BorrowingRequestModel
    {
        [NotNullOrEmpty]
        public string UserEmail { get; set; }

        [NotNullOrEmpty]
        [HasMaxLength(50)]
        public string BookTitle { get; set; }
    }
}
