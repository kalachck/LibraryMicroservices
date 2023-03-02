using LibraryService.DataAccess.Entities;

namespace LibraryService.DataAccess.Repositories.Abstract
{
    public interface IBookRepository : IBaseRepository<Book, ApplicationContext>
    {
        Task<Book> GetByTitleAsync(string title);
    }
}
