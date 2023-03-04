using LibrarySevice.BussinesLogic.DTOs;

namespace LibrarySevice.BussinesLogic.Services.Abstract
{
    public interface IAuthorService
    {
        Task<AuthorDTO> GetAsync(int id);

        Task<bool> UpdateAsync(int id, AuthorDTO author);

        Task<bool> AddAsync(AuthorDTO author);

        Task<bool> DeleteAsync(int id);
    }
}
