using AutoMapper;
using BorrowService.Borrowings.Components.Abstract;
using BorrowService.Borrowings.Entities;
using BorrowService.Borrowings.Exceptions;
using BorrowService.Borrowings.Options;
using BorrowService.Borrowings.Repositories.Abstract;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace BorrowService.Borrowings.Components
{
    public class BorrowingComponent : IBorrowingComponent
    {
        private readonly IBorrowingRepository _repository;
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;
        private readonly CommunicationOptions _options;

        public BorrowingComponent(IBorrowingRepository repository,
            ApplicationContext applicationContext,
            IMapper mapper,
            IOptions<CommunicationOptions> options)
        {
            _repository = repository;
            _applicationContext = applicationContext;
            _mapper = mapper;
            _options = options.Value;
        }

        public async Task<Borrowing> GetAsync(int id)
        {
            try
            {
                var borrowing = await _repository.GetAsync(id);

                if (borrowing != null)
                {
                    return await Task.FromResult(borrowing);
                }

                throw new NotFoundException("Record was not found");
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task<Borrowing> GetByBookIdAsync(int bookId)
        {
            try
            {
                var borrowing = await _repository.GetByBookIdAsync(bookId);

                if (borrowing != null)
                {
                    return await Task.FromResult(borrowing);
                }

                throw new NotFoundException("Record was not found");
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task<Borrowing> GetByEmailAsync(string email)
        {
            try
            {
                var borrowing = await _repository.GetByEmailAsync(email);

                if (borrowing != null)
                {
                    return await Task.FromResult(borrowing);
                }

                throw new NotFoundException("Record was not found");
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task<string> BorrowAsync(string email, int bookId, HttpClient client)
        {
            try
            {
                var identityResult = await client.GetAsync($"{_options.Identity}{email}");
                var libraryResult = await client.GetAsync($"{_options.Library}{bookId}");

                if (identityResult.StatusCode == System.Net.HttpStatusCode.OK && identityResult.Content != null)
                {
                    if (libraryResult.StatusCode == System.Net.HttpStatusCode.OK && libraryResult.Content != null)
                    {
                        var borrowing = new Borrowing()
                        {
                            UserEmail = email,
                            BookId = bookId,
                            AddingDate = DateTime.UtcNow.Date,
                            ExpirationDate = DateTime.UtcNow.Date + TimeSpan.FromDays(30),
                        };

                        _repository.Add(borrowing);

                        await _applicationContext.SaveChangesAsync();

                        return await Task.FromResult($"Book was borrowed successfully by user: {email}");
                    }

                    throw new NotFoundException("Book was not found");
                }

                throw new NotFoundException("User was not found");
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task<string> UpdateAsync(int id, Borrowing borrowing)
        {
            try
            {
                var borrowingEntity = await _repository.GetAsync(id);

                if (borrowingEntity != null)
                {
                    borrowingEntity = _mapper.Map<Borrowing>(borrowing);

                    borrowingEntity.Id = id;

                    _repository.Update(borrowingEntity);

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
                var borrowing = await _repository.GetAsync(id);

                if (borrowing != null)
                {
                    _repository.Delete(borrowing);

                    return await Task.FromResult("The record was successfully deleted");
                }

                throw new NotFoundException("Record was not found");
            }
            catch (SqlException)
            {
                throw;
            }
        }
    }
}
