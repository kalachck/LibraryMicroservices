using BorrowService.Borrowings.Options;
using Microsoft.Extensions.Options;

namespace BorrowService.UnitTests
{
    public class GrpcServiceTests
    {
        private readonly CommunicationOptions _options;

        public GrpcServiceTests(IOptions<CommunicationOptions> options)
        {
            _options = options.Value;
        }

        [Fact]
        public async Task CheckBook_ShouldReturnBookResponse()
        {

        }
    }
}
