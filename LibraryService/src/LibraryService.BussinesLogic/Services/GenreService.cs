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
            var genre = _repository.GetAsync(id);

            if (genre != null)
            {
                return await Task.FromResult(_mapper.Map<GenreDTO>(genre));
            }

            throw new NotFoundException("Record was not found");
        }

        public async Task<string> AddAsync(GenreDTO genre)
        {
            try
            {
                _repository.AddAsync(_mapper.Map<Genre>(genre));

                await _applicationContext.SaveChangesAsync();

                return await Task.FromResult("The record was successfully added");
            }
            catch (SqlException)
            {
                return await Task.FromResult("The record was not added. There were technical problems");
            }
        }

        public async Task<string> UpdateAsync(int id, GenreDTO genre)
        {
            var genreEntity = _repository.GetAsync(id);

            if (genreEntity != null)
            {
                genreEntity = _mapper.Map<Genre>(genre);

                genreEntity.Id = id;

                try
                {
                    _repository.UpdateAsync(genreEntity);

                    await _applicationContext.SaveChangesAsync();

                    return await Task.FromResult("The record was successfully updated");
                }
                catch (SqlException)
                {
                    return await Task.FromResult("The record was not updated. There were technical problems");
                }
            }

            throw new NotFoundException("Record was not found");
        }

        public async Task<string> DeleteAsync(int id)
        {
            var genre = _repository.GetAsync(id);

            if (genre != null)
            {
                try
                {
                    _repository.DeleteAsync(genre);

                    await _applicationContext.SaveChangesAsync();

                    return await Task.FromResult("The record was successfully deleted");
                }
                catch (SqlException)
                {
                    return await Task.FromResult("The record was not deleted. There were technical problems");
                }
            }

            throw new NotFoundException("Record was not found");
        }
    }
}
