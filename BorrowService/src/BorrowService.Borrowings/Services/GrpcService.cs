using BorrowService.Borrowings.Options;
using BorrowService.Borrowings.Services.Abstract;
using BorrwoService.Borrowing;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;

namespace BorrowService.Borrowings.Services
{
    public class GrpcService : IGrpcService
    {
        private readonly CommunicationOptions _options;

        public GrpcService(IOptions<CommunicationOptions> options)
        {
            _options = options.Value;
        }

        public async Task<BookResponse> CheckBook(int bookId)
        {
            var channel = GrpcChannel.ForAddress(_options.Library);

            var client = new CheckBook.CheckBookClient(channel);

            BookResponse response = await client.CheckAsync(new RequestId() { Id = bookId });

            return await Task.FromResult(response);
        }

        public async Task<bool> CheckUser(string email)
        {
            var channel = GrpcChannel.ForAddress(_options.Identity);

            var client = new CheckUser.CheckUserClient(channel);

            UserResponse response = await client.CheckAsync(new RequestEmail() { Email = email });

            return await Task.FromResult(response.IsExists);
        }
    }
}
