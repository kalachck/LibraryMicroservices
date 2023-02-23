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
            try
            {
                var publisher = await _repository.GetAsync(id);

                if (publisher != null)
                {
                    return await Task.FromResult(_mapper.Map<PublisherDTO>(publisher));
                }

                throw new NotFoundException("Record was not found");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> AddAsync(PublisherDTO publisher)
        {
            try
            {
                _repository.Add(_mapper.Map<Publisher>(publisher));

                await _applicationContext.SaveChangesAsync();

                return await Task.FromResult("The record was successfully added");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> UpdateAsync(int id, PublisherDTO publisher)
        {
            try
            {
                var publisherEntity = await _repository.GetAsync(id);

                if (publisherEntity != null)
                {
                    publisherEntity = _mapper.Map<Publisher>(publisher);

                    publisherEntity.Id = id;

                    _repository.Update(publisherEntity);

                    await _applicationContext.SaveChangesAsync();

                    return await Task.FromResult("The record was successfully updated");
                }

                throw new NotFoundException("Record was not found");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> DeleteAsync(int id)
        {
            try
            {
                var publisher = await _repository.GetAsync(id);

                if (publisher != null)
                {

                    _repository.Delete(publisher);

                    await _applicationContext.SaveChangesAsync();

                    return await Task.FromResult("The record was successfully deleted");
                }

                throw new NotFoundException("Record was not found");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
