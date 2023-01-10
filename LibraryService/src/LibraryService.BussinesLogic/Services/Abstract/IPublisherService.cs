using LibrarySevice.BussinesLogic.DTOs;

namespace LibrarySevice.BussinesLogic.Services.Abstract
{
    public interface IPublisherService
    {
        Task<List<PublisherDTO>> TakeAsync(int amount);

        Task<PublisherDTO> GetAsync(int id);

        Task<PublisherDTO> UpdateAsync(int id, PublisherDTO publisher);

        Task<PublisherDTO> AddAsync(PublisherDTO publisher);

        Task<PublisherDTO> DeleteAsync(int id);
    }
}
