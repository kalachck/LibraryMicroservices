using BorrowService.Borrowings.Components.Abstract;
using BorrowService.Borrowings.Entities;
using BorrowService.Borrowings.Exceptions;
using BorrowService.Borrowings.Options;
using BorrowService.Borrowings.Repositories.Abstract;
using BorrowService.RabbitMq.Services.Abstract;
using BorrwoService.Borrowing;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BorrowService.Borrowings.Components
{
    public class BorrowingComponent : IBorrowingComponent
    {
        private readonly IBorrowingRepository _repository;
        private readonly IRabbitService _rabbitService;
        private readonly CommunicationOptions _options;
        private HttpClientHandler _clientHandler;

        public BorrowingComponent(IBorrowingRepository repository,
            IOptions<CommunicationOptions> options,
            IRabbitService rabbitService)
        {
            _repository = repository;
            _rabbitService = rabbitService;
            _options = options.Value;
            _clientHandler = new HttpClientHandler();

            _clientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
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

            var bookId = int.Parse(book["id"]);

            var borrowing = new Borrowing()
            {
                UserEmail = email,
                BookId = bookId,
                BookTitle = title,
                AddingDate = DateTime.Now.Date,
                ExpirationDate = DateTime.Now.Date.AddDays(period),
            };

            _repository.Add(borrowing);

            await _repository.SaveChangesAsync();

            await _rabbitService.LockAsync(bookId);

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

            await _rabbitService.UnlockAsync(borrowing.BookId);

            return await Task.FromResult(true);
        }

        public async Task<bool> CheckUserAsync(string email)
        {
            var channel = GrpcChannel.ForAddress(_options.Identity, new GrpcChannelOptions()
            {
                HttpHandler = _clientHandler
            });

            var client = new CheckUser.CheckUserClient(channel);

            UserResponse response = await client.CheckAsync(new RequestEmail() { Email = email });

            return await Task.FromResult(response.IsExists);
        }

        public async Task<Dictionary<string, string>> GetBookAsync(string title)
        {
            var channel = GrpcChannel.ForAddress(_options.Library, new GrpcChannelOptions()
            {
                HttpHandler = _clientHandler
            });

            var client = new GetBook.GetBookClient(channel);

            BookResponse response = await client.GetAsync(new RequestTitle() { Title = title });

            var book = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Book);

            return await Task.FromResult(book);
        }

        public async Task<bool> CheckBookAsync(int bookId)
        {
            var channel = GrpcChannel.ForAddress(_options.Library, new GrpcChannelOptions()
            {
                HttpHandler = _clientHandler
            });

            var client = new CheckBook.CheckBookClient(channel);

            CheckBookResponse response = await client.CheckAsync(new RequestId() { Id = bookId });

            return await Task.FromResult(response.IsExist);
        }
    }
}
