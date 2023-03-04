using LibrarySevice.BussinesLogic.DTOs;

namespace LibrarySevice.BussinesLogic.Services.Abstract
{
    public interface IPublisherService
    {
        Task<PublisherDTO> GetAsync(int id);

        Task<bool> UpdateAsync(int id, PublisherDTO publisher);

        Task<bool> AddAsync(PublisherDTO publisher);

        Task<bool> DeleteAsync(int id);
    }
}
