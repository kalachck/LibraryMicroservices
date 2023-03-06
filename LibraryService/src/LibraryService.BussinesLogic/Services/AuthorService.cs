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
        private readonly IDbManager<Author> _dbManager;
        private readonly IMapper _mapper;

        public AuthorService(IBaseRepository<Author, ApplicationContext> repository,
            IDbManager<Author> dbManager,
            IMapper mapper)
        {
            _repository = repository;
            _dbManager = dbManager;
            _mapper = mapper;
        }

        public async Task<AuthorDTO> GetAsync(int id)
        {
            var author = await _repository.GetAsync(id);

            if (author == null)
            {
                throw new NotFoundException("Record was not found");
            }

            return await Task.FromResult(_mapper.Map<AuthorDTO>(author));
        }

        public async Task<bool> AddAsync(AuthorDTO author)
        {
            _repository.Add(_mapper.Map<Author>(author));

            await _dbManager.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(int id, AuthorDTO author)
        {
            var authorEntity = await _repository.GetAsync(id);

            if (authorEntity == null)
            {
                throw new NotFoundException("Record was not found");
            }

            authorEntity = _mapper.Map<Author>(author);

            authorEntity.Id = id;

            _repository.Update(authorEntity);

            await _dbManager.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _repository.GetAsync(id);

            if (author == null)
            {
                throw new NotFoundException("Record was not found");
            }

            _repository.Delete(author);

            await _dbManager.SaveChangesAsync();

            return await Task.FromResult(true);
        }
    }
}
