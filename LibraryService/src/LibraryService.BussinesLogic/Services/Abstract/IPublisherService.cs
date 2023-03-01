using LibraryService.BussinesLogic.DTOs;

namespace LibraryService.BussinesLogic.Services.Abstract
{
    public interface IPublisherService
    {
        Task<PublisherDTO> GetAsync(int id);

        Task<string> UpdateAsync(int id, PublisherDTO publisher);

        Task<string> AddAsync(PublisherDTO publisher);

        Task<string> DeleteAsync(int id);
    }
}
