using LibrarySevice.BussinesLogic.DTOs.Abstract;

namespace LibrarySevice.BussinesLogic.DTOs
{
    public class BookAuthorDTO : BaseDTO
    {
        public int BookId { get; set; }

        public BookDTO Book { get; set; }

        public int AuthorId { get; set; }

        public AuthorDTO Author { get; set; }
    }
}
