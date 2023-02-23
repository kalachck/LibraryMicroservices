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

                await _applicationContext.SaveChangesAsync();

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
                var genre = await _repository.GetAsync(id);

                if (genre != null)
                {
                    _repository.Delete(genre);

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
