using BorrowService.Borrowings.Components.Abstract;
using BorrowService.Borrowings.Entities;
using BorrowService.Borrowings.Enums;
using BorrowService.Borrowings.Exceptions;
using BorrowService.Borrowings.Repositories.Abstract;
using BorrowService.Borrowings.Services.Abstract;
using Newtonsoft.Json;

namespace BorrowService.Borrowings.Components
{
    public class BorrowingComponent : IBorrowingComponent
    {
        private readonly IBorrowingRepository _repository;
        private readonly ApplicationContext _applicationContext;
        private readonly IHangfireService _hangfireService;
        private readonly IRabbitService _rabbitService;
        private readonly IGrpcService _grpcService;

        public BorrowingComponent(IBorrowingRepository repository,
            ApplicationContext applicationContext,
            IHangfireService hangfireService,
            IRabbitService rabbitService,
            IGrpcService grpcService)
        {
            _repository = repository;
            _applicationContext = applicationContext;
            _hangfireService = hangfireService;
            _rabbitService = rabbitService;
            _grpcService = grpcService;
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
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Borrowing> GetByEmailAndBookIdAsync(string email, int bookId)
        {
            try
            {
                var borrowing = await _repository.GetByEmailAndBookIdAsync(email, bookId);

                if (borrowing != null)
                {
                    return await Task.FromResult(borrowing);
                }

                throw new NotFoundException("Record was not found");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> BorrowAsync(string email, int bookId)
        {
            try
            {
                var identityResult = await _grpcService.CheckUser(email);
                var libraryResult = await _grpcService.CheckBook(bookId);

                if (identityResult == true)
                {
                    if (libraryResult.IsAvailable == true)
                    {
                        var borrowing = new Borrowing()
                        {
                            UserEmail = email,
                            BookId = bookId,
                            BookTitle = libraryResult.BookTitle,
                            AddingDate = DateTime.Now.Date,
                            ExpirationDate = DateTime.Now.Date.AddDays(30),
                        };

                        _repository.Add(borrowing);

                        await _applicationContext.SaveChangesAsync();

                        _hangfireService.Run();

                        var message = new RabbitMessage()
                        {
                            Topic = Topic.Borrow,
                            BookId = bookId,
                        };

                        _rabbitService.SendMessage(JsonConvert.SerializeObject(message));

                        return await Task.FromResult($"Book was borrowed successfully by user: {email}");
                    }

                    throw new NotAvailableException("Book is not available now");
                }

                throw new NotFoundException("User was not found");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> ExtendAsync(string email, int bookId)
        {
            try
            {
                var borrowing = await _repository.GetByEmailAndBookIdAsync(email, bookId);

                if (borrowing != null)
                {
                    borrowing.AddingDate = DateTime.Now.Date;

                    borrowing.ExpirationDate = DateTime.Now.AddDays(30);

                    _repository.Update(borrowing);

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

        public async Task<string> DeleteByEmailAndBookIdAsync(string email, int bookId)
        {
            try
            {
                var borrowing = await _repository.GetByEmailAndBookIdAsync(email, bookId);

                if (borrowing != null)
                {
                    _repository.Delete(borrowing);

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

        public async Task<string> DeleteAsync(int id)
        {
            try
            {
                var borrowing = await _repository.GetAsync(id);

                if (borrowing != null)
                {
                    _repository.Delete(borrowing);

                    await _applicationContext.SaveChangesAsync();

                    var message = new RabbitMessage()
                    {
                        Topic = Topic.Delete,
                        BookId = borrowing.BookId,
                    };

                    _rabbitService.SendMessage(JsonConvert.SerializeObject(message));

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
