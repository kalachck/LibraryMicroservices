using AutoMapper;
using LibraryService.BussinesLogic.DTOs;
using LibraryService.BussinesLogic.Exceptions;
using LibraryService.BussinesLogic.Services.Abstract;
using LibraryService.DataAccess;
using LibraryService.DataAccess.Entities;
using LibraryService.DataAccess.Repositories.Abstract;

namespace LibraryService.BussinesLogic.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IBaseRepository<Publisher, ApplicationContext> _repository;
        private readonly IDbManager<Publisher> _dbManager;
        private readonly IMapper _mapper;

        public PublisherService(IBaseRepository<Publisher, ApplicationContext> repository,
            IDbManager<Publisher> dbManager,
            IMapper mapper)
        {
            _repository = repository;
            _dbManager = dbManager;
            _mapper = mapper;
        }

        public async Task<PublisherDTO> GetAsync(int id)
        {
            var publisher = await _repository.GetAsync(id);

            if (publisher == null)
            {
                throw new NotFoundException("Record was not found");
            }

            return await Task.FromResult(_mapper.Map<PublisherDTO>(publisher));
        }

        public async Task<bool> AddAsync(PublisherDTO publisher)
        {
            _repository.Add(_mapper.Map<Publisher>(publisher));
                
            await _dbManager.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(int id, PublisherDTO publisher)
        {
            var publisherEntity = await _repository.GetAsync(id);

            if (publisherEntity == null)
            {
                throw new NotFoundException("Record was not found");
            }

            publisherEntity = _mapper.Map<Publisher>(publisher);

            publisherEntity.Id = id;

            _repository.Update(publisherEntity);

            await _dbManager.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var publisher = await _repository.GetAsync(id);

            if (publisher == null)
            {
                throw new NotFoundException("Record was not found");
            }

            _repository.Delete(publisher);

            await _dbManager.SaveChangesAsync();

            return await Task.FromResult(true);
        }
    }
}
