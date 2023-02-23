using LibraryService.BussinesLogic.DTOs;

namespace LibraryService.BussinesLogic.Services.Abstract
{
    public interface IBookService
    {
        Task<BookDTO> GetAsync(int id);

        Task<string> AddAsync(BookDTO book);

        Task<string> UpdateAsync(int id, BookDTO book);

        Task<string> DeleteAsync(int id);

        Task ChangeStatus(string message);
    }
}
