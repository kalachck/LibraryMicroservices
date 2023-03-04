using LibrarySevice.BussinesLogic.DTOs;

namespace LibrarySevice.BussinesLogic.Services.Abstract
{
    public interface IBookService
    {
        Task<BookDTO> GetAsync(int id);

        Task<BookDTO> GetByTitleAsync(string title);

        Task<bool> AddAsync(BookDTO book);

        Task<bool> UpdateAsync(int id, BookDTO book);

        Task<bool> DeleteAsync(int id);
    }
}
