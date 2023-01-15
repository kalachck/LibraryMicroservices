using AutoMapper;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Exceptions;
using LibrarySevice.BussinesLogic.Services.Abstract;
using LibrarySevice.DataAccess;
using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories.Abstract;
using Microsoft.Data.SqlClient;

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
            var publisher = _repository.GetAsync(id);

            if (publisher != null)
            {
                return await Task.FromResult(_mapper.Map<PublisherDTO>(publisher));
            }

            throw new NotFoundException("Record was not found");
        }

        public async Task<string> AddAsync(PublisherDTO publisher)
        {
            try
            {
                _repository.AddAsync(_mapper.Map<Publisher>(publisher));

                await _applicationContext.SaveChangesAsync();

                return await Task.FromResult("The record was successfully added");
            }
            catch (SqlException)
            {
                return await Task.FromResult("The record was not added. There were technical problems");
            }
        }

        public async Task<string> UpdateAsync(int id, PublisherDTO publisher)
        {
            var publisherEntity = _repository.GetAsync(id);

            if (publisherEntity != null)
            {
                publisherEntity = _mapper.Map<Publisher>(publisher);

                publisherEntity.Id = id;

                try
                {
                    _repository.UpdateAsync(publisherEntity);

                    await _applicationContext.SaveChangesAsync();

                    return await Task.FromResult("The record was successfully updated");
                }
                catch (SqlException)
                {
                    return await Task.FromResult("The record was not updated. There were technical problems");
                }
            }

            throw new NotFoundException("Record was not found");
        }

        public async Task<string> DeleteAsync(int id)
        {
            var publisher = _repository.GetAsync(id);

            if (publisher != null)
            {
                try
                {
                    _repository.DeleteAsync(publisher);

                    await _applicationContext.SaveChangesAsync();

                    return await Task.FromResult("The record was successfully deleted");
                }
                catch (Exception)
                {
                    return await Task.FromResult("The record was not deleted. There were technical problems");
                }
            }

            throw new NotFoundException("Record was not found");
        }
    }
}
