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
            try
            {
                var author = _repository.GetAsync(id);

                if (author != null)
                {
                    return await Task.FromResult(_mapper.Map<AuthorDTO>(author));
                }

                throw new NotFoundException("Record was not found");
            }
            catch (SqlException)
            {
                throw;
            }
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
                throw;
            }
        }

        public async Task<string> UpdateAsync(int id, AuthorDTO author)
        {
            try
            {
                var authorEntity = _repository.GetAsync(id);

                if (authorEntity != null)
                {
                    authorEntity = _mapper.Map<Author>(author);

                    authorEntity.Id = id;

                    _repository.UpdateAsync(authorEntity);

                    await _applicationContext.SaveChangesAsync();

                    return await Task.FromResult("The record was successfully updated");
                }

                throw new NotFoundException("Record was not found");
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task<string> DeleteAsync(int id)
        {
            try
            {
                var author = _repository.GetAsync(id);

                if (author != null)
                {

                    _repository.DeleteAsync(author);

                    await _applicationContext.SaveChangesAsync();

                    return await Task.FromResult("The record was successfully deleted");
                }

                throw new NotFoundException("Record was not found");
            }
            catch (SqlException)
            {
                throw;
            }
        }
    }
}
