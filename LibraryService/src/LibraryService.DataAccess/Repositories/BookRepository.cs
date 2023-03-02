using LibraryService.DataAccess.Entities;
using LibraryService.DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.DataAccess.Repositories
{
    public class BookRepository :IBookRepository
    {
        private readonly DbSet<Book> _books;

        public BookRepository(ApplicationContext context)
        {
            _books = context.Books;
        }

        public async Task<Book> GetByTitleAsync(string title)
        {
            return await _books.AsNoTracking().FirstOrDefaultAsync(x => x.Title.ToLower() == title.ToLower() && x.IsAvailable == true);
        }

        public async Task<Book> GetAsync(int id)
        {
            return await _books.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Add(Book entity)
        {
            _books.Add(entity);
        }

        public void Update(Book entity)
        {
            _books.Update(entity);
        }

        public void Delete(Book entity)
        {
            _books.Remove(entity);
        }
    }
}
