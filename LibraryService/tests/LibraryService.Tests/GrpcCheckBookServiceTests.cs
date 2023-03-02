using AutoFixture;
using FluentAssertions;
using Grpc.Core;
using Grpc.Core.Testing;
using LibraryService.BussinesLogic;
using LibraryService.BussinesLogic.DTOs;
using LibraryService.BussinesLogic.Exceptions;
using LibraryService.BussinesLogic.Services;
using LibraryService.BussinesLogic.Services.Abstract;
using Moq;

namespace LibraryService.UnitTests
{
    public class GrpcCheckBookServiceTests
    {
        private readonly Mock<IBookService> _bookService;
        private readonly Fixture _fixture;

        public GrpcCheckBookServiceTests()
        {
            _bookService = new Mock<IBookService>();
            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Theory]
        [InlineData(1)]
        public async Task Check_ShouldReturnCheckBookResponse(int id)
        {
            //Arrange
            _bookService.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<BookDTO>());

            var checkBookService = new GrpcCheckBookService(_bookService.Object);

            var callContext = TestServerCallContext.Create(
                                             method: nameof(checkBookService.Check),
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
            var actualResult = await checkBookService.Check(new RequestId() { Id = id }, callContext);

            //Assert
            actualResult.Should().BeOfType<CheckBookResponse>();
            actualResult.IsExist.Should().BeTrue();
        }

        [Theory]
        [InlineData(1)]
        public async Task Check_ShouldThrowException(int id)
        {
            //Arrange
            _bookService.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();

            var checkBookService = new GrpcCheckBookService(_bookService.Object);

            var callContext = TestServerCallContext.Create(
                                             method: nameof(checkBookService.Check),
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
            var act = () => checkBookService.Check(new RequestId() { Id = 1 }, callContext);

            //Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
