using LibraryService.DataAccess.Entities.Abstract;

namespace LibraryService.DataAccess.Entities
{
    public class Author : Base
    {
        public string Name { get; set; }

        public IList<Book> Books { get; set; }
    }
}
