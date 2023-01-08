using AutoMapper;
using LibrarySevice.BussinesLogic.DTOs.Abstract;
using LibrarySevice.BussinesLogic.Exceptions;
using LibrarySevice.DataAccess.Entities.Abstract;
using LibrarySevice.DataAccess.Repositories.Abstract;

namespace LibrarySevice.BussinesLogic.Services.Abstract
{
    public abstract class BaseDtoService<TDto, TEntity, TRepository> : IBaseDtoService<TDto>
        where TDto : BaseDTO
        where TEntity : BaseEntity
        where TRepository : IBaseRepository<TEntity>
    {
        private readonly TRepository _repository;
        private readonly IMapper _mapper;

        public BaseDtoService(TRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TDto>> TakeAsync(int amount)
        {
            var dtos = await _repository.TakeAsync(amount);

            if (dtos != null)
            {
                return _mapper.Map<List<TDto>>(dtos);
            }

            throw new NotFoundException("Not a single record was found");
        }

        public async Task<TDto> GetAsync(int id)
        {
            var dto = await _repository.GetAsync(id);

            if (dto != null)
            {
                return await Task.FromResult(_mapper.Map<TDto>(dto));
            }

            throw new NotFoundException("Record was not found");
        }

        public async Task<TDto> UpsertAsync(TDto dto)
        {
            if (dto != null)
            {
                var updatedDto = await _repository.UpsertAsync(_mapper.Map<TEntity>(dto));

                return await Task.FromResult(_mapper.Map<TDto>(updatedDto));
            }

            throw new ArgumentNullException("Null argument, check parameters");
        }

        public async Task<TDto> DeleteAsync(int id)
        {
            var dto = await _repository.GetAsync(id);

            if (dto != null)
            {
                return await Task.FromResult(_mapper.Map<TDto>(await _repository.DeleteAsync(dto)));
            }

            throw new NotFoundException("Record was not found");
        }
    }
}
