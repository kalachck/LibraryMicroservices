using AutoFixture;
using BorrowService.Borrowings;
using BorrowService.Borrowings.Components;
using BorrowService.Borrowings.Entities;
using BorrowService.Borrowings.Exceptions;
using BorrowService.Borrowings.Repositories.Abstract;
using BorrowService.Borrowings.Services.Abstract;
using BorrwoService.Borrowing;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Org.BouncyCastle.Asn1.BC;

namespace BorrowService.UnitTests
{
    public class BorrowingComponentTests
    {
        private readonly Mock<IBorrowingRepository> _repository;
        private readonly Mock<ApplicationContext> _context;
        private readonly Mock<IHangfireService> _hangfireService;
        private readonly Mock<IRabbitService> _rabbitService;
        private readonly Mock<IGrpcService> _grpcService;
        private readonly Fixture _fixture;

        public BorrowingComponentTests()
        {
            _repository = new Mock<IBorrowingRepository>();
            _context = new Mock<ApplicationContext>();
            _hangfireService = new Mock<IHangfireService>();
            _rabbitService = new Mock<IRabbitService>();
            _grpcService = new Mock<IGrpcService>();
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldReturnBorrowing(int id)
        {
            //Arrange
            _repository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Borrowing>());

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object, 
                _hangfireService.Object, 
                _rabbitService.Object, 
                _grpcService.Object);

            //Act
            var actualResult = await borrowingComponent.GetAsync(id);

            //Assert
            actualResult.Should().NotBeNull();
            actualResult.Should().BeOfType<Borrowing>();
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldThrowNotFoundException(int id)
        {
            //Arrange
            _repository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync((Borrowing)null);

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            //Assert
            borrowingComponent.Invoking(x => x.GetAsync(id)).Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldThrowException(int id)
        {
            //Arrange
            _repository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            //Assert
            borrowingComponent.Invoking(x => x.GetAsync(id)).Should().ThrowAsync<Exception>();
        }

        [Theory]
        [InlineData(1)]
        public async Task GetByBookIdAsync_ShouldReturnBorrowing(int bookId)
        {
            //Arrange
            _repository.Setup(x => x.GetByBookIdAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Borrowing>());

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            var actualResult = await borrowingComponent.GetByBookIdAsync(bookId);

            //Assert
            actualResult.Should().NotBeNull();
            actualResult.Should().BeOfType<Borrowing>();
        }

        [Theory]
        [InlineData(1)]
        public async Task GetByBookIdAsync_ShouldThrowNotFoundException(int id)
        {
            //Arrange
            _repository.Setup(x => x.GetByBookIdAsync(It.IsAny<int>())).ReturnsAsync((Borrowing)null);

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            //Assert
            borrowingComponent.Invoking(x => x.GetByBookIdAsync(id)).Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public async Task GetByBookIdAsync_ShouldThrowException(int id)
        {
            //Arrange
            _repository.Setup(x => x.GetByBookIdAsync(It.IsAny<int>())).Throws<Exception>();

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            //Assert
            borrowingComponent.Invoking(x => x.GetByBookIdAsync(id)).Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldReturnBorrowing()
        {
            //Arrange
            _repository.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(_fixture.Create<Borrowing>());

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            var actualResult = await borrowingComponent.GetByEmailAsync("testEmail");

            //Assert
            actualResult.Should().NotBeNull();
            actualResult.Should().BeOfType<Borrowing>();
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldThrowNotFoundException()
        {
            //Arrange
            _repository.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((Borrowing)null);

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            //Assert
            borrowingComponent.Invoking(x => x.GetByEmailAsync("testEmail"))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldThrowException()
        {
            //Arrange
            _repository.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).Throws<Exception>();

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            //Assert
            borrowingComponent.Invoking(x => x.GetByEmailAsync("testEmail"))
                .Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task GetByEmailAndBookIdAsync_ShouldReturnBorrowing()
        {
            //Arrange
            _repository.Setup(x => x.GetByEmailAndBookIdAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(_fixture.Create<Borrowing>());

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            var actualResult = await borrowingComponent.GetByEmailAndBookIdAsync("testEmail", 1);

            //Assert
            actualResult.Should().NotBeNull();
            actualResult.Should().BeOfType<Borrowing>();
        }

        [Fact]
        public async Task GetByEmailAndBookIdAsync_ShouldThrowNotFoundException()
        {
            //Arrange
            _repository.Setup(x => x.GetByEmailAndBookIdAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync((Borrowing)null);

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            //Assert
            borrowingComponent.Invoking(x => x.GetByEmailAndBookIdAsync("testEmail", 1))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetByEmailAndBookIdAsync_ShouldThrowException()
        {
            //Arrange
            _repository.Setup(x => x.GetByEmailAndBookIdAsync(It.IsAny<string>(), It.IsAny<int>())).Throws<Exception>();

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            //Assert
            borrowingComponent.Invoking(x => x.GetByEmailAndBookIdAsync("testEmail", 1))
                .Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task BorrowAsync_ShouldReturnSuccessfullMessage()
        {
            //Arrange
            _grpcService.Setup(x => x.CheckUser(It.IsAny<string>())).ReturnsAsync(true);
            _grpcService.Setup(x => x.CheckBook(It.IsAny<int>())).ReturnsAsync(new BookResponse()
            {
                BookTitle = "testTitle",
                IsAvailable = true,
            });

            _repository.Setup(x => x.Add(It.IsAny<Borrowing>()));

            _hangfireService.Setup(x => x.Run());

            _rabbitService.Setup(x => x.SendMessage(It.IsAny<string>()));

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            var actualResult = await borrowingComponent.BorrowAsync("testEmail", 1);

            //Assert
            actualResult.Should().NotBeNull();
            actualResult.Should().BeOfType<string>("Book was borrowed successfully by user: testEmail");
        }

        [Fact]
        public async Task BorrowAsync_ShouldThrowNotAvailableException()
        {
            //Arrange
            _grpcService.Setup(x => x.CheckUser(It.IsAny<string>())).ReturnsAsync(true);
            _grpcService.Setup(x => x.CheckBook(It.IsAny<int>())).ReturnsAsync(new BookResponse() 
            { 
                BookTitle = "testTitle",
                IsAvailable = false,
            });

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            //Assert
            borrowingComponent.Invoking(x => x.BorrowAsync("testEmail", 1))
                .Should().ThrowAsync<NotAvailableException>();
        }

        [Fact]
        public async Task BorrowAsync_ShouldThrowNotFoundException()
        {
            //Arrange
            _grpcService.Setup(x => x.CheckUser(It.IsAny<string>())).ReturnsAsync(false);

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            //Assert
            borrowingComponent.Invoking(x => x.BorrowAsync("testEmail", 1))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task BorrowAsync_ShouldThrowException()
        {
            //Arrange
            _grpcService.Setup(x => x.CheckUser(It.IsAny<string>())).Throws<Exception>();

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            //Assert
            borrowingComponent.Invoking(x => x.BorrowAsync("testEmail", 1))
                .Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task ExtendAsync_ShouldReturnSuccessfullMessage()
        {
            //Arrange
            _repository.Setup(x => x.GetByEmailAndBookIdAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(_fixture.Create<Borrowing>());
            _repository.Setup(x => x.Update(It.IsAny<Borrowing>()));

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            var actualResult = await borrowingComponent.ExtendAsync("testEmail", 1);

            //Assert
            actualResult.Should().NotBeNull();
            actualResult.Should().BeOfType<string>("The record was successfully updated");
        }

        [Fact]
        public async Task ExtendAsync_ShouldThrowNotFoundException()
        {
            //Arrange
            _repository.Setup(x => x.GetByEmailAndBookIdAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync((Borrowing)null);

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            //Assert
            borrowingComponent.Invoking(x => x.ExtendAsync("testEmail", 1))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task ExtendAsync_ShouldThrowException()
        {
            //Arrange
            _repository.Setup(x => x.GetByEmailAndBookIdAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Throws<Exception>();

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            //Assert
            borrowingComponent.Invoking(x => x.ExtendAsync("testEmail", 1))
                .Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task DeleteByEmailAndBookIdAsync_ShouldReturnSuccessfullMessage()
        {
            //Arrange
            _repository.Setup(x => x.GetByEmailAndBookIdAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(_fixture.Create<Borrowing>());
            _repository.Setup(x => x.Delete(It.IsAny<Borrowing>()));

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            var actualResult = await borrowingComponent.DeleteByEmailAndBookIdAsync("testEmail", 1);

            //Assert
            actualResult.Should().NotBeNull();
            actualResult.Should().BeOfType<string>("The record was successfully deleted");
        }

        [Fact]
        public async Task DeleteByEmailAndBookIdAsync_ShouldThrowNotFoundException()
        {
            //Arrange
            _repository.Setup(x => x.GetByEmailAndBookIdAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync((Borrowing)null);

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            //Assert
            borrowingComponent.Invoking(x => x.DeleteByEmailAndBookIdAsync("testEmail", 1))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteByEmailAndBookIdAsync_ShouldThrowException()
        {
            //Arrange
            _repository.Setup(x => x.GetByEmailAndBookIdAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Throws<Exception>();

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            //Assert
            borrowingComponent.Invoking(x => x.DeleteByEmailAndBookIdAsync("testEmail", 1))
                .Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnSuccessfullMessage()
        {
            //Arrange
            _repository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.Create<Borrowing>());
            _repository.Setup(x => x.Delete(It.IsAny<Borrowing>()));

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            var actualResult = await borrowingComponent.DeleteAsync(1);

            //Assert
            actualResult.Should().NotBeNull();
            actualResult.Should().BeOfType<string>("The record was successfully deleted");
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowNotFoundException()
        {
            //Arrange
            _repository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((Borrowing)null);

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            //Assert
            borrowingComponent.Invoking(x => x.DeleteAsync(1))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException()
        {
            //Arrange
            _repository.Setup(x => x.GetAsync(It.IsAny<int>()))
                .Throws<Exception>();

            var borrowingComponent = new BorrowingComponent(
                _repository.Object,
                _context.Object,
                _hangfireService.Object,
                _rabbitService.Object,
                _grpcService.Object);

            //Act
            //Assert
            borrowingComponent.Invoking(x => x.DeleteAsync(1))
                .Should().ThrowAsync<Exception>();
        }
    }
}
