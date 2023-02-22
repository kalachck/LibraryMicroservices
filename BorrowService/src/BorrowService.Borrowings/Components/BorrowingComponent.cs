using BorrowService.Borrowings.Components.Abstract;
using BorrowService.Borrowings.Entities;
using BorrowService.Borrowings.Enums;
using BorrowService.Borrowings.Exceptions;
using BorrowService.Borrowings.Options;
using BorrowService.Borrowings.Repositories.Abstract;
<<<<<<< HEAD
using BorrowService.Borrowings.Services.Abstract;
=======
>>>>>>> remotes/origin/BorrowServiceImplementation
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace BorrowService.Borrowings.Components
{
    public class BorrowingComponent : IBorrowingComponent
    {
        private readonly IBorrowingRepository _repository;
        private readonly HttpClient _client;
        private readonly CommunicationOptions _options;

        public BorrowingComponent(IBorrowingRepository repository,
            IOptions<CommunicationOptions> options,
            IHttpClientFactory clientFactory)
        {
            _repository = repository;
            _client = clientFactory.CreateClient();
            _options = options.Value;
        }

        public async Task<Borrowing> GetAsync(string email, string title)
        {
            var borrowing = await _repository.GetByEmailAndTitleAsync(email, title);

            if (borrowing == null)
            {
                throw new NotFoundException("Record was not found");
            }

            return await Task.FromResult(borrowing);
        }

        public async Task<bool> BorrowAsync(string email, string title, int period)
        {
            var existingBorrowing = await _repository.GetByEmailAndTitleAsync(email, title);

            if (existingBorrowing != null)
            {
                throw new AlreadyExistsException("This record already exists");
            }

            if (await CheckUserAsync(email) == false)
            {
                throw new NotFoundException("User was not found");
            }

            var book = await GetBookAsync(title);

            var borrowing = new Borrowing()
            {
                UserEmail = email,
                BookId = int.Parse(book["id"]),
                BookTitle = title,
                AddingDate = DateTime.Now.Date,
                ExpirationDate = DateTime.Now.Date.AddDays(period),
            };

            _repository.Add(borrowing);

            await _repository.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> ExtendAsync(string email, string title, int period)
        {
            var borrowing = await _repository.GetByEmailAndTitleAsync(email, title);

            if (borrowing == null)
            {
                throw new NotFoundException("Record was not found");
            }

            if (await CheckUserAsync(email) == false)
            {
                throw new NotFoundException("User was not found");
            }

            if (await CheckBookAsync(borrowing.BookId) == false)
            {
                throw new NotFoundException("Book was not found");
            }

            borrowing.ExpirationDate = borrowing.ExpirationDate.AddDays(period);

            _repository.Update(borrowing);

            await _repository.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(string email, string title)
        {
            var borrowing = await _repository.GetByEmailAndTitleAsync(email, title);

            if (borrowing == null)
            {
                throw new NotFoundException("Record was not found");
            }

            if (await CheckUserAsync(email) == false)
            {
                throw new NotFoundException("User was not found");
            }

            if (await CheckBookAsync(borrowing.BookId) == false)
            {
                throw new NotFoundException("Book was not found");
            }

            _repository.Delete(borrowing);

            await _repository.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> CheckUserAsync(string email)
        {
            var identityResult = await _client.GetAsync($"{_options.Identity}{email}");

            if (identityResult.StatusCode != HttpStatusCode.OK)
            {
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public async Task<Dictionary<string, string>> GetBookAsync(string title)
        {
            var libraryResult = await _client.GetAsync($"{_options.LibraryByTitle}{title}");

            if (libraryResult.StatusCode != HttpStatusCode.OK)
            {
                throw new NotFoundException("Book was not found");
            }

            var libraryContent = await libraryResult.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(libraryContent);
        }

        public async Task<bool> CheckBookAsync(int bookId)
        {
            var libraryResult = await _client.GetAsync($"{_options.LibraryById}{bookId}");

            if (libraryResult.StatusCode != HttpStatusCode.OK)
            {
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }
    }
}
