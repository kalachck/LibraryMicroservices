using FluentAssertions;
using Grpc.Core;
using Grpc.Core.Testing;
using LibrarySevice.BussinesLogic;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Services;
using LibrarySevice.BussinesLogic.Services.Abstract;
using Moq;

namespace LibraryService.UnitTests
{
    public class CheckBookServiceTests
    {
        private readonly Mock<IBookService> _bookService;

        public CheckBookServiceTests()
        {
            _bookService = new Mock<IBookService>();
        }

        [Theory]
        [InlineData(1)]
        public async Task Check_ShouldReturnBookResponse(int id)
        {
            //Arrange
            _bookService.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(new BookDTO()
            {
                Title = "testTitle",
                PublicationDate = DateTime.Now,
            });

            var checkBookService = new CheckBookService(_bookService.Object);

            var callContext = TestServerCallContext.Create(
                                             method: nameof(checkBookService.Check)
                                            , host: "localhost"
                                            , deadline: DateTime.Now.AddMinutes(30)
                                            , requestHeaders: new Metadata()
                                            , cancellationToken: CancellationToken.None
                                            , peer: "10.0.0.25:5001"
                                            , authContext: null
                                            , contextPropagationToken: null
                                            , writeHeadersFunc: (metadata) => Task.CompletedTask
                                            , writeOptionsGetter: () => new WriteOptions()
                                            , writeOptionsSetter: (writeOptions) => { }
                                            );

            //Act
            var actualResult = await checkBookService.Check(new RequestId() { Id = id }, callContext);

            //Assert
            actualResult.Should().BeOfType<BookResponse>();
            actualResult.IsAvailable.Should().BeTrue();
            actualResult.BookTitle.Should().BeOfType<string>("testTitle");
        }

        [Theory]
        [InlineData(1)]
        public async Task Check_ShouldThrowException(int id)
        {
            //Arrange
            _bookService.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();

            var checkBookService = new CheckBookService(_bookService.Object);

            var callContext = TestServerCallContext.Create(
                                             method: nameof(checkBookService.Check)
                                            , host: "localhost"
                                            , deadline: DateTime.Now.AddMinutes(30)
                                            , requestHeaders: new Metadata()
                                            , cancellationToken: CancellationToken.None
                                            , peer: "10.0.0.25:5001"
                                            , authContext: null
                                            , contextPropagationToken: null
                                            , writeHeadersFunc: (metadata) => Task.CompletedTask
                                            , writeOptionsGetter: () => new WriteOptions()
                                            , writeOptionsSetter: (writeOptions) => { }
                                            );

            //Act
            //Assert
            checkBookService.Invoking(x => x.Check(new RequestId() { Id = id }, callContext))
                .Should().ThrowAsync<Exception>();
        }
    }
}
