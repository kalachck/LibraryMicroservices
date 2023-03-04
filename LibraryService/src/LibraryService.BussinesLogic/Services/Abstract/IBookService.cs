using LibraryService.BussinesLogic.DTOs;

namespace LibraryService.BussinesLogic.Services.Abstract
{
    public interface IBookService
    {
        Task<BookDTO> GetAsync(int id);

        Task<BookDTO> GetByTitleAsync(string title);

        Task<bool> AddAsync(BookDTO book);

        Task<bool> UpdateAsync(int id, BookDTO book);

        Task<bool> DeleteAsync(int id);

        Task LockAsync(string message);

        Task UnlockAsync(string message);
    }
}
