using AutoMapper;
using BorrowService.Borrowings.Components.Abstract;
using BorrowService.Borrowings.Entities;
using BorrowService.Borrowings.Exceptions;
using BorrowService.Borrowings.Repositories.Abstract;
using Npgsql.TypeMapping;

namespace BorrowService.Borrowings.Components
{
    public class BorrowingComponent : IBorrowingComponent
    {
        private readonly IBorrowingRepository _repository;
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public BorrowingComponent(IBorrowingRepository repository,
            ApplicationContext applicationContext,
            IMapper mapper)
        {
            _repository = repository;
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public async Task<List<BorrowingEntity>> TakeAsync(int amount)
        {
            var borrowings = await _repository.TakeAsync(amount);

            if (borrowings != null)
            {
                return await Task.FromResult(borrowings);
            }

            throw new NotFoundException("Not a single record was found");
        }

        public async Task<BorrowingEntity> GetAsync(int id)
        {
            var borrowing = await _repository.GetAsync(id);

            if (borrowing != null)
            {
                return await Task.FromResult(borrowing);
            }

            throw new NotFoundException("Record was not found");
        }

        public async Task<BorrowingEntity> GetByBookIdAsync(int bookId)
        {
            var borrowing = await _repository.GetByBookIdAsync(bookId);

            if (borrowing != null)
            {
                return await Task.FromResult(borrowing);
            }

            throw new NotFoundException("Record was not found");
        }

        public async Task<BorrowingEntity> GetByEmailAsync(string email)
        {
            var borrowing = await _repository.GetByEmailAsync(email);

            if (borrowing != null)
            {
                return await Task.FromResult(borrowing);
            }

            throw new NotFoundException("Record was not found");
        }

        public async Task<BorrowingEntity> AddAsync(BorrowingEntity borrowing)
        {
            var result = await _repository.AddAsync(borrowing);

            await _applicationContext.SaveChangesAsync();

            return await Task.FromResult(result);
        }

        public async Task<BorrowingEntity> UpdateAsync(int id, BorrowingEntity borrowing)
        {
            var borrowingEntity = await _repository.GetAsync(id);

            if (borrowingEntity != null)
            {
                borrowingEntity = _mapper.Map<BorrowingEntity>(borrowing);

                borrowingEntity.Id = id;

                var result = await _repository.UpdateAsync(borrowingEntity);

                await _applicationContext.SaveChangesAsync();

                return await Task.FromResult(result);
            }

            throw new ArgumentNullException("Null argument, check parameters");
        }

        public async Task<BorrowingEntity> DeleteAsync(int id)
        {
            var borrowing = await _repository.GetAsync(id);

            if (borrowing != null)
            {
                return await Task.FromResult(await _repository.DeleteAsync(borrowing));
            }

            throw new NotFoundException("Record was not found");
        }
    }
}
