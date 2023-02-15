using AutoMapper;
using FluentAssertions;
using LibrarySevice.Api.Mappings;
using LibrarySevice.BussinesLogic;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Exceptions;
using LibrarySevice.BussinesLogic.Services;
using LibrarySevice.DataAccess;
using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories.Abstract;
using Moq;
using Newtonsoft.Json;

namespace LibraryService.UnitTests
{
    public class BookServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ApplicationContext> _context;
        private readonly Mock<IBaseRepository<Book, ApplicationContext>> _bookRepository;

        public BookServiceTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BookProfile>();
            });

            _mapper = new Mapper(configuration);
            _context = new Mock<ApplicationContext>();
            _bookRepository = new Mock<IBaseRepository<Book, ApplicationContext>>();

            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldReturnAuthorDTO(int id)
        {
            //Arrange
            _bookRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(new Book()
            {
                Id = id,
                Title = "testTitle",
                PublicationDate = DateTime.Now,
            });

            var bookService = new BookService(_bookRepository.Object, _context.Object, _mapper);

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

            var bookService = new BookService(_bookRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            bookService.Invoking(async x => x.GetAsync(id)).Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public void GetAsync_ShouldThrowException(int id)
        {
            //Arrange
            _bookRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();

            var bookService = new BookService(_bookRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            bookService.Invoking(async x => x.GetAsync(id)).Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task AddAsync_ShouldReturnSuccessfullMessage()
        {
            //Arrange
            _bookRepository.Setup(x => x.Add(It.IsAny<Book>()));

            var bookService = new BookService(_bookRepository.Object, _context.Object, _mapper);

            //Act
            var actualResult = await bookService.AddAsync(new BookDTO()
            {
                Title = "testTitle",
                PublicationDate = DateTime.Now,
            });

            //Assert
            actualResult.Should().BeOfType<string>("The record was successfully added");
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException()
        {
            //Arrange
            _bookRepository.Setup(x => x.Add(It.IsAny<Book>())).Throws<Exception>();

            var bookService = new BookService(_bookRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            bookService.Invoking(async x => x.AddAsync(new BookDTO()
            {
                Title = "testTitle",
                PublicationDate = DateTime.Now,
            })).Should().ThrowAsync<Exception>();
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldReturnSuccessfullMessage(int id)
        {
            //Arrange
            _bookRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(new Book()
            {
                Id = id,
                Title = "testTitle",
                PublicationDate = DateTime.Now,
                IsAvailable = true,
            });
            _bookRepository.Setup(x => x.Update(It.IsAny<Book>()));

            var bookService = new BookService(_bookRepository.Object, _context.Object, _mapper);

            //Act
            var actualResult = await bookService.UpdateAsync(id, new BookDTO()
            {
                Title = "testTitle",
                PublicationDate = DateTime.Now,
                IsAvailable = true,
            });

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

            var bookService = new BookService(_bookRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            bookService.Invoking(async x => x.UpdateAsync(id, new BookDTO()
            {
                Title = "testTitle",
                PublicationDate = DateTime.Now,
            })).Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldThrowException(int id)
        {
            //Arrange
            _bookRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();
            _bookRepository.Setup(x => x.Update(It.IsAny<Book>()));

            var bookService = new BookService(_bookRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            bookService.Invoking(x => x.UpdateAsync(id, new BookDTO()
            {
                Title = "testTitle",
                PublicationDate = DateTime.Now,
            })).Should().ThrowAsync<Exception>();
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldReturnSuccessfullMessage(int id)
        {
            //Arrange
            _bookRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(new Book()
            {
                Id = id,
                Title = "testTitle",
                PublicationDate = DateTime.Now,
                IsAvailable = true,
            });
            _bookRepository.Setup(x => x.Delete(It.IsAny<Book>()));

            var bookService = new BookService(_bookRepository.Object, _context.Object, _mapper);

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

            var bookService = new BookService(_bookRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            bookService.Invoking(async x => x.DeleteAsync(id)).Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldThrowException(int id)
        {
            //Arrange
            _bookRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();
            _bookRepository.Setup(x => x.Delete(It.IsAny<Book>()));

            var bookService = new BookService(_bookRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            bookService.Invoking(async x => x.GetAsync(id)).Should().ThrowAsync<Exception>();
        }


        [Theory]
        [InlineData(1)]
        public async Task ChangeStatusAsync_WhenTopicBorrow_ShouldPassedSuccessfully(int id)
        {
            //Arrange
            var rabbitMessage = JsonConvert.SerializeObject(new RabbitMessage()
            {
                Topic = LibrarySevice.BussinesLogic.Enums.Topic.Borrow,
                BookId = id,
            });

            _bookRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(new Book()
            {
                Id = id,
                Title = "testTitle",
                PublicationDate = DateTime.Now,
            });
            _bookRepository.Setup(x => x.Update(It.IsAny<Book>()));

            var bookService = new BookService(_bookRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            bookService.Invoking(x => x.ChangeStatus(rabbitMessage)).Should().NotThrow<Exception>();
        }
    }
}
