using LibrarySevice.DataAccess.Entities.Abstract;

namespace LibrarySevice.DataAccess.Entities
{
    public class BookEntity : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublicationDate { get; set; }

        public int PageCount { get; set; }

        public int PublisherId { get; set; }

        public PublisherEntity Publisher { get; set; }

        public IList<BookAuthorEntity> BookAuthors { get; set; }
    }
}
