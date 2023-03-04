using AutoMapper;
using LibraryService.BussinesLogic.DTOs;
using LibraryService.BussinesLogic.Exceptions;
using LibraryService.BussinesLogic.Services.Abstract;
using LibraryService.DataAccess;
using LibraryService.DataAccess.Entities;
using LibraryService.DataAccess.Repositories.Abstract;

namespace LibraryService.BussinesLogic.Services
{
    public class GenreService : IGenreService
    {
        private readonly IBaseRepository<Genre, ApplicationContext> _repository;
        private readonly IDbManager<Genre> _dbManager;
        private readonly IMapper _mapper;

        public GenreService(IBaseRepository<Genre, ApplicationContext> repository,
            IDbManager<Genre> dbManager,
            IMapper mapper)
        {
            _repository = repository;
            _dbManager = dbManager;
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
                
            await _dbManager.SaveChangesAsync();

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

            await _dbManager.SaveChangesAsync();

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

            await _dbManager.SaveChangesAsync();

            return await Task.FromResult(true);
        }
    }
}
