using AutoFixture;
using AutoMapper;
using FluentAssertions;
using LibraryService.Api.Mappings;
using LibraryService.BussinesLogic.DTOs;
using LibraryService.BussinesLogic.Exceptions;
using LibraryService.BussinesLogic.Services;
using LibraryService.BussinesLogic.Services.Abstract;
using LibraryService.DataAccess.Entities;
using LibraryService.DataAccess.Repositories;
using Moq;

namespace LibraryService.UnitTests
{
    public class BookServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IDbManager<Book>> _dbManager;
        private readonly Mock<BookRepository> _bookRepository;
        private readonly Fixture _fixture;

        public BookServiceTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BookProfile>();
            });

            _mapper = new Mapper(configuration);
            _dbManager = new Mock<IDbManager<Book>>();
            _bookRepository = new Mock<BookRepository>();

            _fixture = new Fixture();
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldReturnBookDTO(int id)
        {
            //Arrange
            _bookRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Book>());

            var bookService = new BookService(_bookRepository.Object, _dbManager.Object, _mapper);

            //Act
            var actualResult = await bookService.GetAsync(id);

            //Assert
            actualResult.Should().BeOfType<BookDTO>();
            actualResult.Title.Should().Be("testTitle");
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldThrowNotFoundException(int id)
        {
            //Arrange
            _bookRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult((Book)null));

            var bookService = new BookService(_bookRepository.Object, _dbManager.Object, _mapper);

            //Act
            //Assert
            bookService.Invoking(x => x.GetAsync(id)).Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public void GetAsync_ShouldThrowException(int id)
        {
            //Arrange
            _bookRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();

            var bookService = new BookService(_bookRepository.Object, _dbManager.Object, _mapper);

            //Act
            //Assert
            bookService.Invoking(x => x.GetAsync(id)).Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task AddAsync_ShouldReturnSuccessfullMessage()
        {
            //Arrange
            _bookRepository.Setup(x => x.Add(It.IsAny<Book>()));

            var bookService = new BookService(_bookRepository.Object, _dbManager.Object, _mapper);

            //Act
            var actualResult = await bookService.AddAsync(_fixture.Create<BookDTO>());

            //Assert
            actualResult.Should().BeOfType<string>("The record was successfully added");
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException()
        {
            //Arrange
            _bookRepository.Setup(x => x.Add(It.IsAny<Book>())).Throws<Exception>();

            var bookService = new BookService(_bookRepository.Object, _dbManager.Object, _mapper);

            //Act
            //Assert
            bookService.Invoking(x => x.AddAsync(_fixture.Create<BookDTO>()))
                .Should().ThrowAsync<Exception>();
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldReturnSuccessfullMessage(int id)
        {
            //Arrange
            _bookRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Book>());
            _bookRepository.Setup(x => x.Update(It.IsAny<Book>()));

            var bookService = new BookService(_bookRepository.Object, _dbManager.Object, _mapper);

            //Act
            var actualResult = await bookService.UpdateAsync(id, _fixture.Create<BookDTO>());

            //Assert
            actualResult.Should().BeOfType<string>("The record was successfully updated");
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldReturnNotFoundException(int id)
        {
            //Arrange
            _bookRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync((Book)null);
            _bookRepository.Setup(x => x.Update(It.IsAny<Book>()));

            var bookService = new BookService(_bookRepository.Object, _dbManager.Object, _mapper);

            //Act
            //Assert
            bookService.Invoking(x => x.UpdateAsync(id, _fixture.Create<BookDTO>()))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldThrowException(int id)
        {
            //Arrange
            _bookRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();
            _bookRepository.Setup(x => x.Update(It.IsAny<Book>()));

            var bookService = new BookService(_bookRepository.Object, _dbManager.Object, _mapper);

            //Act
            //Assert
            bookService.Invoking(x => x.UpdateAsync(id, _fixture.Create<BookDTO>()))
                .Should().ThrowAsync<Exception>();
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldReturnSuccessfullMessage(int id)
        {
            //Arrange
            _bookRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Book>());
            _bookRepository.Setup(x => x.Delete(It.IsAny<Book>()));

            var bookService = new BookService(_bookRepository.Object, _dbManager.Object, _mapper);

            //Act
            var actualResult = await bookService.DeleteAsync(id);

            //Assert
            actualResult.Should().BeOfType<string>("The record was successfully deleted");
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldThrowNotFoundException(int id)
        {
            //Arrange
            _bookRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult((Book)null));
            _bookRepository.Setup(x => x.Delete(It.IsAny<Book>()));

            var bookService = new BookService(_bookRepository.Object, _dbManager.Object, _mapper);

            //Act
            //Assert
            bookService.Invoking(x => x.DeleteAsync(id)).Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldThrowException(int id)
        {
            //Arrange
            _bookRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();
            _bookRepository.Setup(x => x.Delete(It.IsAny<Book>()));

            var bookService = new BookService(_bookRepository.Object, _dbManager.Object, _mapper);

            //Act
            //Assert
            bookService.Invoking(x => x.GetAsync(id)).Should().ThrowAsync<Exception>();
        }
    }
}
