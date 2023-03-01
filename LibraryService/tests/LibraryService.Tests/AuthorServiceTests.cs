using AutoFixture;
using AutoMapper;
using FluentAssertions;
using LibraryService.Api.Mappings;
using LibraryService.BussinesLogic.DTOs;
using LibraryService.BussinesLogic.Exceptions;
using LibraryService.BussinesLogic.Services;
using LibraryService.BussinesLogic.Services.Abstract;
using LibraryService.DataAccess;
using LibraryService.DataAccess.Entities;
using LibraryService.DataAccess.Repositories.Abstract;
using Moq;

namespace LibraryService.UnitTests
{
    public class AuthorServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IDbManager<Author>> _dbManager;
        private readonly Mock<IBaseRepository<Author, ApplicationContext>> _authorRepository;
        private readonly Fixture _fixture;

        public AuthorServiceTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AuthorProfile>();
                cfg.AddProfile<BookProfile>();
            });

            _mapper = new Mapper(configuration);
            _dbManager = new Mock<IDbManager<Author>>();
            _authorRepository = new Mock<IBaseRepository<Author, ApplicationContext>>();
            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _fixture.Customize<Author>(x => x.Without(p =>p.Books));
            _fixture.Customize<AuthorDTO>(x => x.Without(p => p.Books));
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldReturnAuthorDTO(int id)
        {
            //Arrange
            _authorRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Author>());

            var authorService = new AuthorService(_authorRepository.Object, _dbManager.Object, _mapper);

            //Act
            var actualResult = await authorService.GetAsync(id);

            //Assert
            actualResult.Should().BeOfType<AuthorDTO>();
            actualResult.Should().NotBeNull();
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldThrowNotFoundException(int id)
        {
            //Arrange
            _authorRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult((Author)null));

            var authorService = new AuthorService(_authorRepository.Object, _dbManager.Object, _mapper);

            //Act
            //Assert
            authorService.Invoking(x => x.GetAsync(id)).Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public void GetAsync_ShouldThrowException(int id)
        {
            //Arrange
            _authorRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();

            var authorService = new AuthorService(_authorRepository.Object, _dbManager.Object, _mapper);

            //Act
            //Assert
            authorService.Invoking(x => x.GetAsync(id)).Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task AddAsync_ShouldReturnSuccessfullMessage()
        {
            //Arrange
            _authorRepository.Setup(x => x.Add(It.IsAny<Author>()));

            var authorService = new AuthorService(_authorRepository.Object, _dbManager.Object, _mapper);

            //Act
            var actualResult = await authorService.AddAsync(_fixture.Create<AuthorDTO>());

            //Assert
            actualResult.Should().BeOfType<string>("The record was successfully added");
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException()
        {
            //Arrange
            _authorRepository.Setup(x => x.Add(It.IsAny<Author>())).Throws<Exception>();

            var authorService = new AuthorService(_authorRepository.Object, _dbManager.Object, _mapper);

            //Act
            //Assert
            authorService.Invoking(x => x.AddAsync(_fixture.Create<AuthorDTO>()))
                .Should().ThrowAsync<Exception>();
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldReturnSuccessfullMessage(int id)
        {
            //Arrange
            _authorRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Author>());
            _authorRepository.Setup(x => x.Update(It.IsAny<Author>()));

            var authorService = new AuthorService(_authorRepository.Object, _dbManager.Object, _mapper);

            //Act
            var actualResult = await authorService.UpdateAsync(id, _fixture.Create<AuthorDTO>());

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

            var authorService = new AuthorService(_authorRepository.Object, _dbManager.Object, _mapper);

            //Act
            //Assert
            authorService.Invoking(x => x.UpdateAsync(id, _fixture.Create<AuthorDTO>()))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldThrowException(int id)
        {
            //Arrange
            _authorRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();
            _authorRepository.Setup(x => x.Update(It.IsAny<Author>()));

            var authorService = new AuthorService(_authorRepository.Object, _dbManager.Object, _mapper);

            //Act
            //Assert
            authorService.Invoking(x => x.UpdateAsync(id, _fixture.Create<AuthorDTO>()))
                .Should().ThrowAsync<Exception>();
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldReturnSuccessfullMessage(int id)
        {
            //Arrange
            _authorRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Author>());
            _authorRepository.Setup(x => x.Delete(It.IsAny<Author>()));

            var authorService = new AuthorService(_authorRepository.Object, _dbManager.Object, _mapper);

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

            var authorService = new AuthorService(_authorRepository.Object, _dbManager.Object, _mapper);

            //Act
            //Assert
            authorService.Invoking(x => x.DeleteAsync(id)).Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldThrowException(int id)
        {
            //Arrange
            _authorRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();
            _authorRepository.Setup(x => x.Delete(It.IsAny<Author>()));

            var authorService = new AuthorService(_authorRepository.Object, _dbManager.Object, _mapper);

            //Act
            //Assert
            authorService.Invoking(x => x.GetAsync(id)).Should().ThrowAsync<Exception>();
        }
    }
}
