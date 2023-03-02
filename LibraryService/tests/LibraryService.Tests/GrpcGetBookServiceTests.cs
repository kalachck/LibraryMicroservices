using AutoFixture;
using Grpc.Core.Testing;
using Grpc.Core;
using LibraryService.BussinesLogic.DTOs;
using LibraryService.BussinesLogic.Services;
using LibraryService.BussinesLogic.Services.Abstract;
using Moq;
using LibraryService.BussinesLogic;
using FluentAssertions;

namespace LibraryService.UnitTests
{
    public class GrpcGetBookServiceTests
    {
        private readonly Mock<IBookService> _bookService;
        private readonly Fixture _fixture;

        public GrpcGetBookServiceTests()
        {
            _bookService = new Mock<IBookService>();
            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task Get_ShouldReturnBookResponse()
        {
            //Arrange
            _bookService.Setup(x => x.GetByTitleAsync(It.IsAny<string>())).ReturnsAsync(_fixture.Create<BookDTO>());

            var getBookService = new GrpcGetBookService(_bookService.Object);

            var callContext = TestServerCallContext.Create(
                                             method: nameof(getBookService.Get),
                                             host: "localhost",
                                             deadline: DateTime.Now.AddMinutes(30),
                                             requestHeaders: new Metadata(),
                                             cancellationToken: CancellationToken.None,
                                             peer: "10.0.0.25:5001",
                                             authContext: null,
                                             contextPropagationToken: null,
                                             writeHeadersFunc: (metadata) => Task.CompletedTask,
                                             writeOptionsGetter: () => new WriteOptions(),
                                             writeOptionsSetter: (writeOptions) => { }
                                            );

            //Act
            var actualResult = await getBookService.Get(new RequestTitle() { Title = "testTitle" }, callContext);

            //Assert
            actualResult.Should().BeOfType<BookResponse>();
            actualResult.Book.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_ShouldThrowException()
        {
            //Arrange
            _bookService.Setup(x => x.GetByTitleAsync(It.IsAny<string>())).Throws<Exception>();

            var getBookService = new GrpcGetBookService(_bookService.Object);

            var callContext = TestServerCallContext.Create(
                                             method: nameof(getBookService.Get),
                                             host: "localhost",
                                             deadline: DateTime.Now.AddMinutes(30),
                                             requestHeaders: new Metadata(),
                                             cancellationToken: CancellationToken.None,
                                             peer: "10.0.0.25:5001",
                                             authContext: null,
                                             contextPropagationToken: null,
                                             writeHeadersFunc: (metadata) => Task.CompletedTask,
                                             writeOptionsGetter: () => new WriteOptions(),
                                             writeOptionsSetter: (writeOptions) => { }
                                            );

            //Act
            var act = () => getBookService.Get(new RequestTitle() { Title = "testTitle" }, callContext);

            //Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
