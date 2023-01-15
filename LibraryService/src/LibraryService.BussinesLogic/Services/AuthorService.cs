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
    public class AuthorService : IAuthorService
    {
        private readonly IBaseRepository<Author, ApplicationContext> _repository;
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public AuthorService(IBaseRepository<Author, ApplicationContext> repository,
            ApplicationContext applicationContext,
            IMapper mapper)
        {
            _repository = repository;
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public async Task<AuthorDTO> GetAsync(int id)
        {
            var author = _repository.GetAsync(id);

            if (author != null)
            {
                return await Task.FromResult(_mapper.Map<AuthorDTO>(author));
            }

            throw new NotFoundException("Record was not found");
        }

        public async Task<string> AddAsync(AuthorDTO author)
        {
            try
            {
                _repository.AddAsync(_mapper.Map<Author>(author));

                await _applicationContext.SaveChangesAsync();

                return await Task.FromResult("The record was successfully added");
            }
            catch (SqlException)
            {
                return await Task.FromResult("The record was not added. There were technical problems");
            }
        }

        public async Task<string> UpdateAsync(int id, AuthorDTO author)
        {
            var authorEntity = _repository.GetAsync(id);

            if (authorEntity != null)
            {
                authorEntity = _mapper.Map<Author>(author);

                authorEntity.Id = id;

                try
                {
                    _repository.UpdateAsync(authorEntity);

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
            var author = _repository.GetAsync(id);

            if (author != null)
            {
                try
                {
                    _repository.DeleteAsync(author);

                    await _applicationContext.SaveChangesAsync();

                    return await Task.FromResult("The record was successfully deleted");
                }
                catch (SqlException)
                {
                    return await Task.FromResult("The record was not deleted. There were technical problems");
                }
            }

            throw new NotFoundException("Record was not found");
        }
    }
}
