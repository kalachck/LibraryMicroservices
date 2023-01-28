using BorrowService.Borrowings.Components.Abstract;
using BorrowService.Borrowings.Entities;
using BorrowService.Borrowings.Enums;
using BorrowService.Borrowings.Exceptions;
using BorrowService.Borrowings.Options;
using BorrowService.Borrowings.Repositories.Abstract;
using BorrowService.Borrowings.Services.Abstract;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BorrowService.Borrowings.Components
{
    public class BorrowingComponent : IBorrowingComponent
    {
        private readonly IBorrowingRepository _repository;
        private readonly ApplicationContext _applicationContext;
        private readonly IHangfireService _hangfireService;
        private readonly IRabbitService _rabbitService;
        private readonly CommunicationOptions _options;

        public BorrowingComponent(IBorrowingRepository repository,
            ApplicationContext applicationContext,
            IOptions<CommunicationOptions> options,
            IHangfireService hangfireService,
            IRabbitService rabbitService)
        {
            _repository = repository;
            _applicationContext = applicationContext;
            _hangfireService = hangfireService;
            _rabbitService = rabbitService;
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
                        var book = JsonConvert.DeserializeObject<Dictionary<string, string>>(await libraryResult.Content.ReadAsStringAsync());

                        if (book["isAvailable"] == "true")
                        {
                            var identity = await identityResult.Content.ReadAsStringAsync();

                            var borrowing = new Borrowing()
                            {
                                UserEmail = email,
                                BookId = bookId,
                                BookTitle = book["title"],
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

                    throw new NotFoundException("Book was not found");
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
