using LibrarySevice.DataAccess.Entities.Abstract;

namespace LibrarySevice.DataAccess.Entities
{
    public class BookAuthorEntity : BaseEntity
    {
        public int BookId { get; set; }

        public BookEntity Book { get; set; }

        public int AuthorId { get; set; }

        public AuthorEntity Author { get; set; }
    }
}
