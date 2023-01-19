using LibrarySevice.BussinesLogic.DTOs;

namespace LibrarySevice.BussinesLogic.Services.Abstract
{
    public interface IBookService
    {
        Task<BookDTO> GetAsync(int id);

        Task<string> AddAsync(BookDTO book);

        Task<string> UpdateAsync(int id, BookDTO book);

        Task<string> DeleteAsync(int id);

        void ChangeStatus(string message);
    }
}
