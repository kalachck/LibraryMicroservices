using LibrarySevice.DataAccess.Entities.Abstract;

namespace LibrarySevice.DataAccess.Entities
{
    public class GenreEntity : BaseEntity
    {
        public string Title { get; set; }

        public IList<BookEntity> Books { get; set; }
    }
}
