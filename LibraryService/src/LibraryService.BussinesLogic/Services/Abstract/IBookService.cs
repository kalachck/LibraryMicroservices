using LibrarySevice.BussinesLogic.DTOs;

namespace LibrarySevice.BussinesLogic.Services.Abstract
{
    public interface IBookService
    {
        Task<List<BookDTO>> TakeAsync(int amount);

        Task<BookDTO> GetAsync(int id);

        Task<BookDTO> UpdateAsync(int id, BookDTO book);

        Task<BookDTO> AddAsync(BookDTO book);

        Task<BookDTO> DeleteAsync(int id);
    }
}
