using LibrarySevice.BussinesLogic.DTOs;

namespace LibrarySevice.BussinesLogic.Services.Abstract
{
    public interface IAuthorService
    {
        Task<List<AuthorDTO>> TakeAsync(int amount);

        Task<AuthorDTO> GetAsync(int id);

        Task<AuthorDTO> UpdateAsync(int id, AuthorDTO author);

        Task<AuthorDTO> AddAsync(AuthorDTO author);

        Task<AuthorDTO> DeleteAsync(int id);
    }
}
