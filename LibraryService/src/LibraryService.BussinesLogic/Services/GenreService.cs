using AutoMapper;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Exceptions;
using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories;
using LibrarySevice.DataAccess;
using LibrarySevice.BussinesLogic.Services.Abstract;

namespace LibrarySevice.BussinesLogic.Services
{
    public class GenreService : IGenreService
    {
        private readonly GenreRepository _repository;
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public GenreService(GenreRepository repository,
            ApplicationContext applicationContext,
            IMapper mapper)
        {
            _repository = repository;
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public async Task<List<GenreDTO>> TakeAsync(int amount)
        {
            var genres = await _repository.TakeAsync(amount);

            if (genres != null)
            {
                return _mapper.Map<List<GenreDTO>>(genres);
            }

            throw new NotFoundException("Not a single record was found");
        }

        public async Task<GenreDTO> GetAsync(int id)
        {
            var genre = await _repository.GetAsync(id);

            if (genre != null)
            {
                return await Task.FromResult(_mapper.Map<GenreDTO>(genre));
            }

            throw new NotFoundException("Record was not found");
        }

        public async Task<GenreDTO> AddAsync(GenreDTO genre)
        {
            var result = await _repository.AddAsync(_mapper.Map<GenreEntity>(genre));

            await _applicationContext.SaveChangesAsync();

            return await Task.FromResult(_mapper.Map<GenreDTO>(result));
        }

        public async Task<GenreDTO> UpdateAsync(int id, GenreDTO genre)
        {
            var genreEntity = await _repository.GetAsync(id);

            if (genreEntity != null)
            {
                genreEntity = _mapper.Map<GenreEntity>(genre);

                genreEntity.Id = id;

                var result = await _repository.UpdateAsync(genreEntity);

                await _applicationContext.SaveChangesAsync();

                return await Task.FromResult(_mapper.Map<GenreDTO>(result));
            }

            throw new NotFoundException("Record was not found");
        }

        public async Task<GenreDTO> DeleteAsync(int id)
        {
            var genre = await _repository.GetAsync(id);

            if (genre != null)
            {
                var result = await _repository.DeleteAsync(genre);

                await _applicationContext.SaveChangesAsync();

                return await Task.FromResult(_mapper.Map<GenreDTO>(result));
            }

            throw new NotFoundException("Record was not found");
        }
    }
}
