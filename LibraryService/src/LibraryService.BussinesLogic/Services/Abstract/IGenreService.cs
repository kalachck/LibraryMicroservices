using LibrarySevice.BussinesLogic.DTOs;

namespace LibrarySevice.BussinesLogic.Services.Abstract
{
    public interface IGenreService
    {
        Task<List<GenreDTO>> TakeAsync(int amount);

        Task<GenreDTO> GetAsync(int id);

        Task<GenreDTO> UpdateAsync(int id, GenreDTO genre);

        Task<GenreDTO> AddAsync(GenreDTO genre);

        Task<GenreDTO> DeleteAsync(int id);
    }
}
