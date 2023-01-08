using LibrarySevice.BussinesLogic.DTOs.Abstract;

namespace LibrarySevice.BussinesLogic.Services.Abstract
{
    public interface IBaseDtoService<TDto> where TDto : BaseDTO
    {
        Task<List<TDto>> TakeAsync(int amount);

        Task<TDto> GetAsync(int id);

        Task<TDto> UpsertAsync(TDto dto);

        Task<TDto> DeleteAsync(int id);
    }
}
