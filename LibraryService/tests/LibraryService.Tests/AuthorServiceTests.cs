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

namespace LibraryService.UnitTests
{
    public class AuthorServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ApplicationContext> _context;
        private readonly Mock<IBaseRepository<Author, ApplicationContext>> _authorRepository;

        public AuthorServiceTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AuthorProfile>();
            });

            _mapper = new Mapper(configuration);
            _context = new Mock<ApplicationContext>();
            _authorRepository = new Mock<IBaseRepository<Author, ApplicationContext>>();

            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldReturnAuthorDTO(int id)
        {
            //Arrange
            _authorRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(new Author()
            {
                Id = id,
                Name = "testName",
                Books = null,
            });

            var authorService = new AuthorService(_authorRepository.Object, _context.Object, _mapper);

            //Act
            var actualResult = await authorService.GetAsync(id);

            //Assert
            actualResult.Should().BeOfType<AuthorDTO>();
            actualResult.Name.Should().Be("testName");
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldThrowNotFoundException(int id)
        {
            //Arrange
            _authorRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult((Author)null));

            var authorService = new AuthorService(_authorRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            authorService.Invoking(async x => x.GetAsync(id)).Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public void GetAsync_ShouldThrowException(int id)
        {
            //Arrange
            _authorRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();

            var authorService = new AuthorService(_authorRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            authorService.Invoking(async x => x.GetAsync(id)).Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task AddAsync_ShouldReturnSuccessfullMessage()
        {
            //Arrange
            _authorRepository.Setup(x => x.Add(It.IsAny<Author>()));

            var authorService = new AuthorService(_authorRepository.Object, _context.Object, _mapper);

            //Act
            var actualResult = await authorService.AddAsync(new AuthorDTO()
            {
                Name = "testName",
                Books = null,
            });

            //Assert
            actualResult.Should().BeOfType<string>("The record was successfully added");
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException()
        {
            //Arrange
            _authorRepository.Setup(x => x.Add(It.IsAny<Author>())).Throws<Exception>();

            var authorService = new AuthorService(_authorRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            authorService.Invoking(async x => x.AddAsync(new AuthorDTO()
            {
                Name = "testName",
                Books = null,
            })).Should().ThrowAsync<Exception>();
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldReturnSuccessfullMessage(int id)
        {
            //Arrange
            _authorRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(new Author()
            {
                Id = id,
                Name = "testName",
                Books = null,
            });
            _authorRepository.Setup(x => x.Update(It.IsAny<Author>()));

            var authorService = new AuthorService(_authorRepository.Object, _context.Object, _mapper);

            //Act
            var actualResult = await authorService.UpdateAsync(id, new AuthorDTO()
            {
                Name = "testName2",
                Books = null,
            });

            //Assert
            actualResult.Should().BeOfType<string>("The record was successfully updated");
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldReturnNotFoundException(int id)
        {
            //Arrange
            _authorRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync((Author)null);
            _authorRepository.Setup(x => x.Update(It.IsAny<Author>()));

            var authorService = new AuthorService(_authorRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            authorService.Invoking(async x => x.UpdateAsync(id, new AuthorDTO()
            {
                Name = "testName2",
                Books = null,
            })).Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldThrowException(int id)
        {
            //Arrange
            _authorRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();
            _authorRepository.Setup(x => x.Update(It.IsAny<Author>()));

            var authorService = new AuthorService(_authorRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            authorService.Invoking(x => x.UpdateAsync(id, new AuthorDTO()
            {
                Name = "testName",
                Books = null,
            })).Should().ThrowAsync<Exception>();
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldReturnSuccessfullMessage(int id)
        {
            //Arrange
            _authorRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(new Author()
            {
                Id = id,
                Name = "testName",
                Books = null,
            });
            _authorRepository.Setup(x => x.Delete(It.IsAny<Author>()));

            var authorService = new AuthorService(_authorRepository.Object, _context.Object, _mapper);

            //Act
            var actualResult = await authorService.DeleteAsync(id);

            //Assert
            actualResult.Should().BeOfType<string>("The record was successfully deleted");
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldThrowNotFoundException(int id)
        {
            //Arrange
            _authorRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult((Author)null));
            _authorRepository.Setup(x => x.Delete(It.IsAny<Author>()));

            var authorService = new AuthorService(_authorRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            authorService.Invoking(async x => x.DeleteAsync(id)).Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldThrowException(int id)
        {
            //Arrange
            _authorRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();
            _authorRepository.Setup(x => x.Delete(It.IsAny<Author>()));

            var authorService = new AuthorService(_authorRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            authorService.Invoking(async x => x.GetAsync(id)).Should().ThrowAsync<Exception>();
        }
    }
}
