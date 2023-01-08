using LibrarySevice.DataAccess.Entities.Abstract;

namespace LibrarySevice.DataAccess.Entities
{
    public class PublisherEntity : BaseEntity
    {
        public string Title { get; set; }

        public string Address { get; set; }

        public IList<BookEntity> Books { get; set; }
    }
}
