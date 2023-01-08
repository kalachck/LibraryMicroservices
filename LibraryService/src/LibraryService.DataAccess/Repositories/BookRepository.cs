using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories.Abstract;

namespace LibrarySevice.DataAccess.Repositories
{
    public class BookRepository : BaseRepository<BookEntity, ApplicationContext>
    {
        public BookRepository(ApplicationContext context) : base(context)
        { }
    }
}
