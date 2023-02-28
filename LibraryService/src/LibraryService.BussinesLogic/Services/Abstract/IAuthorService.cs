using LibraryService.BussinesLogic.DTOs;

namespace LibraryService.BussinesLogic.Services.Abstract
{
    public interface IAuthorService
    {
        Task<AuthorDTO> GetAsync(int id);

        Task<string> UpdateAsync(int id, AuthorDTO author);

        Task<string> AddAsync(AuthorDTO author);

        Task<string> DeleteAsync(int id);
    }
}
