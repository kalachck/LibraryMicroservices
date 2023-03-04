using LibrarySevice.BussinesLogic.DTOs;

namespace LibrarySevice.BussinesLogic.Services.Abstract
{
    public interface IGenreService
    {
        Task<GenreDTO> GetAsync(int id);

        Task<bool> UpdateAsync(int id, GenreDTO genre);

        Task<bool> AddAsync(GenreDTO genre);

        Task<bool> DeleteAsync(int id);
    }
}
