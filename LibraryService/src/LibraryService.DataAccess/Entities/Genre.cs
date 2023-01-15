using LibrarySevice.DataAccess.Entities.Abstract;

namespace LibrarySevice.DataAccess.Entities
{
    public class Genre : Base
    {
        public string Title { get; set; }

        public IList<Book> Books { get; set; }
    }
}
