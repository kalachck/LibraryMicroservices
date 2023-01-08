using LibrarySevice.Api.Models.Abstract;

namespace LibrarySevice.Api.Models
{
    public class BookAuthorModel : BaseModel
    {
        public int BookId { get; set; }

        public BookModel Book { get; set; }

        public int AuthorId { get; set; }

        public AuthorModel Author { get; set; }
    }
}
