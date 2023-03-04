using AutoMapper;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Exceptions;
using LibrarySevice.BussinesLogic.Services.Abstract;
using LibrarySevice.DataAccess;
using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories.Abstract;

namespace LibrarySevice.BussinesLogic.Services
{
    public class GenreService : IGenreService
    {
        private readonly IBaseRepository<Genre, ApplicationContext> _repository;
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public GenreService(IBaseRepository<Genre, ApplicationContext> repository,
            ApplicationContext applicationContext,
            IMapper mapper)
        {
            _repository = repository;
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public async Task<GenreDTO> GetAsync(int id)
        {
            var genre = await _repository.GetAsync(id);

            if (genre == null)
            {
                throw new NotFoundException("Record was not found");   
            }

            return await Task.FromResult(_mapper.Map<GenreDTO>(genre));
        }

        public async Task<bool> AddAsync(GenreDTO genre)
        {
            _repository.Add(_mapper.Map<Genre>(genre));

            await _applicationContext.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(int id, GenreDTO genre)
        {
            var genreEntity = await _repository.GetAsync(id);

            if (genreEntity == null)
            {
                throw new NotFoundException("Record was not found");
            }

            genreEntity = _mapper.Map<Genre>(genre);

            genreEntity.Id = id;

            _repository.Update(genreEntity);

            await _applicationContext.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var genre = await _repository.GetAsync(id);

            if (genre == null)
            {
                throw new NotFoundException("Record was not found");
            }

            _repository.Delete(genre);

            await _applicationContext.SaveChangesAsync();

            return await Task.FromResult(true);
        }
    }
}
