using AutoMapper;
using LibraryService.BussinesLogic.DTOs;
using LibraryService.BussinesLogic.Exceptions;
using LibraryService.BussinesLogic.Services.Abstract;
using LibraryService.DataAccess;
using LibraryService.DataAccess.Entities;
using LibraryService.DataAccess.Repositories.Abstract;

namespace LibraryService.BussinesLogic.Services
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
                var author = await _repository.GetAsync(id);

                if (author != null)
                {
                    return await Task.FromResult(_mapper.Map<AuthorDTO>(author));
                }

                throw new NotFoundException("Record was not found");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> AddAsync(AuthorDTO author)
        {
            try
            {
                _repository.Add(_mapper.Map<Author>(author));

                await _applicationContext.SaveChangesAsync();

                return await Task.FromResult("The record was successfully added");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> UpdateAsync(int id, AuthorDTO author)
        {
            try
            {
                var authorEntity = await _repository.GetAsync(id);

                if (authorEntity != null)
                {
                    authorEntity = _mapper.Map<Author>(author);

                    authorEntity.Id = id;

                    _repository.Update(authorEntity);

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
                var author = await _repository.GetAsync(id);

                if (author != null)
                {
                    _repository.Delete(author);

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
