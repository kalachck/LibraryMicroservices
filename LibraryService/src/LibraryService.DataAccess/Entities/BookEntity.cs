using LibrarySevice.DataAccess.Entities.Abstract;

namespace LibrarySevice.DataAccess.Entities
{
    public class BookEntity : BaseEntity
    {
        public string Title { get; set; }

        public DateTime PublicationDate { get; set; }

        public int AuthorId { get; set; }

        public AuthorEntity Author { get; set; }

        public int GenreId { get; set; }

        public GenreEntity Genre { get; set; }

        public int PublisherId { get; set; }

        public PublisherEntity Publisher { get; set; }
    }
}
