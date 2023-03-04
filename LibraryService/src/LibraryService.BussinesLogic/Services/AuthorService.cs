using AutoMapper;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Exceptions;
using LibrarySevice.BussinesLogic.Services.Abstract;
using LibrarySevice.DataAccess;
using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories.Abstract;

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

            await _applicationContext.SaveChangesAsync();

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

            await _applicationContext.SaveChangesAsync();

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

            await _applicationContext.SaveChangesAsync();

            return await Task.FromResult(true);
        }
    }
}
