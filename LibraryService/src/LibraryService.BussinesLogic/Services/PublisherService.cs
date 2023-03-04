using AutoMapper;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Exceptions;
using LibrarySevice.BussinesLogic.Services.Abstract;
using LibrarySevice.DataAccess;
using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories.Abstract;

namespace LibrarySevice.BussinesLogic.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IBaseRepository<Publisher, ApplicationContext> _repository;
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public PublisherService(IBaseRepository<Publisher, ApplicationContext> repository,
            ApplicationContext applicationContext,
            IMapper mapper)
        {
            _repository = repository;
            _applicationContext = applicationContext;
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

            await _applicationContext.SaveChangesAsync();

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

            await _applicationContext.SaveChangesAsync();

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

            await _applicationContext.SaveChangesAsync();

            return await Task.FromResult(true);
        }
    }
}
