using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories.Abstract;

namespace LibrarySevice.DataAccess.Repositories
{
    public class BookAuthorRepository : BaseRepository<BookAuthorEntity, ApplicationContext>
    {
        public BookAuthorRepository(ApplicationContext context) : base(context)
        { }
    }
}
