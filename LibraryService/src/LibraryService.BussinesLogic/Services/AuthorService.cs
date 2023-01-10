using AutoMapper;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Exceptions;
using LibrarySevice.BussinesLogic.Services.Abstract;
using LibrarySevice.DataAccess;
using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories;

namespace LibrarySevice.BussinesLogic.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AuthorRepository _repository;
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public AuthorService(AuthorRepository repository,
            ApplicationContext applicationContext,
            IMapper mapper)
        {
            _repository = repository;
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public async Task<List<AuthorDTO>> TakeAsync(int amount)
        {
            var authors = await _repository.TakeAsync(amount);

            if (authors != null)
            {
                return _mapper.Map<List<AuthorDTO>>(authors);
            }

            throw new NotFoundException("Not a single record was found");
        }

        public async Task<AuthorDTO> GetAsync(int id)
        {
            var author = await _repository.GetAsync(id);

            if (author != null)
            {
                return await Task.FromResult(_mapper.Map<AuthorDTO>(author));
            }

            throw new NotFoundException("Record was not found");
        }

        public async Task<AuthorDTO> AddAsync(AuthorDTO author)
        {
            var result = await _repository.AddAsync(_mapper.Map<AuthorEntity>(author));

            await _applicationContext.SaveChangesAsync();

            return await Task.FromResult(_mapper.Map<AuthorDTO>(result));
        }

        public async Task<AuthorDTO> UpdateAsync(int id, AuthorDTO author)
        {
            var authorEntity = await _repository.GetAsync(id);

            if (authorEntity != null)
            {
                authorEntity = _mapper.Map<AuthorEntity>(author);

                authorEntity.Id = id;

                var result = await _repository.UpdateAsync(authorEntity);

                await _applicationContext.SaveChangesAsync();

                return await Task.FromResult(_mapper.Map<AuthorDTO>(result));
            }

            throw new NotFoundException("Record was not found");
        }

        public async Task<AuthorDTO> DeleteAsync(int id)
        {
            var author = await _repository.GetAsync(id);

            if (author != null)
            {
                var result = await _repository.DeleteAsync(author);

                await _applicationContext.SaveChangesAsync();

                return await Task.FromResult(_mapper.Map<AuthorDTO>(result));
            }

            throw new NotFoundException("Record was not found");
        }
    }
}
