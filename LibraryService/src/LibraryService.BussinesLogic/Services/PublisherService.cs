using AutoMapper;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Exceptions;
using LibrarySevice.BussinesLogic.Services.Abstract;
using LibrarySevice.DataAccess;
using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories;

namespace LibrarySevice.BussinesLogic.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly PublisherRepository _repository;
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public PublisherService(PublisherRepository repository,
            ApplicationContext applicationContext,
            IMapper mapper)
        {
            _repository = repository;
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public async Task<List<PublisherDTO>> TakeAsync(int amount)
        {
            var publishers = await _repository.TakeAsync(amount);

            if (publishers != null)
            {
                return _mapper.Map<List<PublisherDTO>>(publishers);
            }

            throw new NotFoundException("Not a single record was found");
        }

        public async Task<PublisherDTO> GetAsync(int id)
        {
            var publisher = await _repository.GetAsync(id);

            if (publisher != null)
            {
                return await Task.FromResult(_mapper.Map<PublisherDTO>(publisher));
            }

            throw new NotFoundException("Record was not found");
        }

        public async Task<PublisherDTO> AddAsync(PublisherDTO publisher)
        {
            var result = await _repository.AddAsync(_mapper.Map<PublisherEntity>(publisher));

            await _applicationContext.SaveChangesAsync();

            return await Task.FromResult(_mapper.Map<PublisherDTO>(result));
        }

        public async Task<PublisherDTO> UpdateAsync(int id, PublisherDTO publisher)
        {
            var publisherEntity = await _repository.GetAsync(id);

            if (publisherEntity != null)
            {
                publisherEntity = _mapper.Map<PublisherEntity>(publisher);

                publisherEntity.Id = id;

                var result = await _repository.UpdateAsync(publisherEntity);

                await _applicationContext.SaveChangesAsync();

                return await Task.FromResult(_mapper.Map<PublisherDTO>(result));
            }

            throw new NotFoundException("Record was not found");
        }

        public async Task<PublisherDTO> DeleteAsync(int id)
        {
            var publisher = await _repository.GetAsync(id);

            if (publisher != null)
            {
                var result = await _repository.DeleteAsync(publisher);

                await _applicationContext.SaveChangesAsync();

                return await Task.FromResult(_mapper.Map<PublisherDTO>(result));
            }

            throw new NotFoundException("Record was not found");
        }
    }
}
