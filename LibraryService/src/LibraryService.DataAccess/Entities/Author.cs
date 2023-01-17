using LibrarySevice.DataAccess.Entities.Abstract;

namespace LibrarySevice.DataAccess.Entities
{
    public class Author : Base
    {
        public string Name { get; set; }

        public IList<Book> Books { get; set; }
    }
}
