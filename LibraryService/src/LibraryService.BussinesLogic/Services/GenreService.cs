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
            try
            {
                var genre = await _repository.GetAsync(id);

                if (genre != null)
                {
                    return await Task.FromResult(_mapper.Map<GenreDTO>(genre));
                }

                throw new NotFoundException("Record was not found");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> AddAsync(GenreDTO genre)
        {
            try
            {
                _repository.Add(_mapper.Map<Genre>(genre));

                await _dbManager.SaveChangesAsync();

                return await Task.FromResult("The record was successfully added");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> UpdateAsync(int id, GenreDTO genre)
        {
            try
            {
                var genreEntity = await _repository.GetAsync(id);

                if (genreEntity != null)
                {
                    genreEntity = _mapper.Map<Genre>(genre);

                    genreEntity.Id = id;

                    _repository.Update(genreEntity);

                    await _dbManager.SaveChangesAsync();

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
                var genre = await _repository.GetAsync(id);

                if (genre != null)
                {
                    _repository.Delete(genre);

                    await _dbManager.SaveChangesAsync();

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
