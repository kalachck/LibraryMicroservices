using LibraryService.DataAccess.Entities.Abstract;

namespace LibraryService.DataAccess.Entities
{
    public class Publisher : Base
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public IList<Book> Books { get; set; }
    }
}
