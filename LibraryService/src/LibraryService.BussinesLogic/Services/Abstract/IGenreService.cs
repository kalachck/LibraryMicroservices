using LibraryService.BussinesLogic.DTOs;

namespace LibraryService.BussinesLogic.Services.Abstract
{
    public interface IGenreService
    {
        Task<GenreDTO> GetAsync(int id);

        Task<string> UpdateAsync(int id, GenreDTO genre);

        Task<string> AddAsync(GenreDTO genre);

        Task<string> DeleteAsync(int id);
    }
}
