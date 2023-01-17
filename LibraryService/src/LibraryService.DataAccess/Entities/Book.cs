using LibrarySevice.DataAccess.Entities.Abstract;

namespace LibrarySevice.DataAccess.Entities
{
    public class Book : Base
    {
        public string Title { get; set; }

        public DateTime PublicationDate { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int? AuthorId { get; set; } = null;

        public Author Author { get; set; }

        public int? GenreId { get; set; } = null;

        public Genre Genre { get; set; }

        public int? PublisherId { get; set; } = null;

        public Publisher Publisher { get; set; }
    }
}
