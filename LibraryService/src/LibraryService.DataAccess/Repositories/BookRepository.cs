using LibraryService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.DataAccess.Repositories
{
    public class BookRepository : BaseRepository<Book, ApplicationContext>
    {
        private readonly DbSet<Book> _books;

        public BookRepository(ApplicationContext context) : base(context)
        {
            _books = context.Books;
        }

        public async Task<Book> GetByTitleAsync(string title)
        {
            return await _books.AsNoTracking().FirstOrDefaultAsync(x => x.Title.ToLower() == title.ToLower() && x.IsAvailable == true);
        }
    }
}
