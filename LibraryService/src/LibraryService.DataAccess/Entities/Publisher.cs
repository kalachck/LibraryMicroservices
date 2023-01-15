using LibrarySevice.DataAccess.Entities.Abstract;

namespace LibrarySevice.DataAccess.Entities
{
    public class Publisher : Base
    {
        public string Title { get; set; }

        public string Address { get; set; }

        public IList<Book> Books { get; set; }
    }
}
