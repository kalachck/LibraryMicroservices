using BorrowService.Borrowings.Entities;
using BorrowService.Borrowings.Repositories.Abstract;
using BorrowService.Borrowings.Components.Abstract;
using BorrowService.Borrowings.Exceptions;
using System.Net;

namespace BorrowService.Borrowings.Components
{
    public class BorrowingComponent : IBorrowingComponent
    {
        private readonly IBorrowingRepository _repository;

        public BorrowingComponent(IBorrowingRepository repository)
        {
            _repository = repository;
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

        public async Task<BorrowingEntity> UpsertAsync(BorrowingEntity entity)
        {
            if (entity != null)
            {
                var updatedDto = await _repository.UpsertAsync(entity);

                return await Task.FromResult(updatedDto);
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
