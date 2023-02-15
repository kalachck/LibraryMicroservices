using AutoMapper;
using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories.Abstract;
using LibrarySevice.DataAccess;
using Moq;
using LibrarySevice.Api.Mappings;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Exceptions;
using LibrarySevice.BussinesLogic.Services;
using FluentAssertions;
using System.Security.AccessControl;
using AutoFixture;

namespace LibraryService.UnitTests
{
    public class GenreServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ApplicationContext> _context;
        private readonly Mock<IBaseRepository<Genre, ApplicationContext>> _genreRepository;
        private readonly Fixture _fixture;

        public GenreServiceTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GenreProfile>();
            });

            _mapper = new Mapper(configuration);
            _context = new Mock<ApplicationContext>();
            _genreRepository = new Mock<IBaseRepository<Genre, ApplicationContext>>();
            _fixture = new Fixture();

            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldReturnGenreDTO(int id)
        {
            //Arrange
            _genreRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Genre>());

            var genreService = new GenreService(_genreRepository.Object, _context.Object, _mapper);

            //Act
            var actualResult = await genreService.GetAsync(id);

            //Assert
            actualResult.Should().BeOfType<GenreDTO>();
            actualResult.Name.Should().Be("testName");
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldThrowNotFoundException(int id)
        {
            //Arrange
            _genreRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult((Genre)null));

            var genreService = new GenreService(_genreRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            genreService.Invoking(x => x.GetAsync(id)).Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public void GetAsync_ShouldThrowException(int id)
        {
            //Arrange
            _genreRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();

            var genreService = new GenreService(_genreRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            genreService.Invoking(x => x.GetAsync(id)).Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task AddAsync_ShouldReturnSuccessfullMessage()
        {
            //Arrange
            _genreRepository.Setup(x => x.Add(It.IsAny<Genre>()));

            var genreService = new GenreService(_genreRepository.Object, _context.Object, _mapper);

            //Act
            var actualResult = await genreService.AddAsync(_fixture.Create<GenreDTO>());

            //Assert
            actualResult.Should().BeOfType<string>("The record was successfully added");
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException()
        {
            //Arrange
            _genreRepository.Setup(x => x.Add(It.IsAny<Genre>())).Throws<Exception>();

            var genreService = new GenreService(_genreRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            genreService.Invoking(x => x.AddAsync(_fixture.Create<GenreDTO>())).Should().ThrowAsync<Exception>();
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldReturnSuccessfullMessage(int id)
        {
            //Arrange
            _genreRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Genre>());
            _genreRepository.Setup(x => x.Update(It.IsAny<Genre>()));

            var genreService = new GenreService(_genreRepository.Object, _context.Object, _mapper);

            //Act
            var actualResult = await genreService.UpdateAsync(id, _fixture.Create<GenreDTO>());

            //Assert
            actualResult.Should().BeOfType<string>("The record was successfully updated");
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldReturnNotFoundException(int id)
        {
            //Arrange
            _genreRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync((Genre)null);
            _genreRepository.Setup(x => x.Update(It.IsAny<Genre>()));

            var genreService = new GenreService(_genreRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            genreService.Invoking(x => x.UpdateAsync(id, _fixture.Create<GenreDTO>()))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldThrowException(int id)
        {
            //Arrange
            _genreRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();
            _genreRepository.Setup(x => x.Update(It.IsAny<Genre>()));

            var genreService = new GenreService(_genreRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            genreService.Invoking(x => x.UpdateAsync(id, _fixture.Create<GenreDTO>()))
                .Should().ThrowAsync<Exception>();
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldReturnSuccessfullMessage(int id)
        {
            //Arrange
            _genreRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Genre>());
            _genreRepository.Setup(x => x.Delete(It.IsAny<Genre>()));

            var genreService = new GenreService(_genreRepository.Object, _context.Object, _mapper);

            //Act
            var actualResult = await genreService.DeleteAsync(id);

            //Assert
            actualResult.Should().BeOfType<string>("The record was successfully deleted");
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldThrowNotFoundException(int id)
        {
            //Arrange
            _genreRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult((Genre)null));
            _genreRepository.Setup(x => x.Delete(It.IsAny<Genre>()));

            var genreService = new GenreService(_genreRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            genreService.Invoking(x => x.DeleteAsync(id)).Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldThrowException(int id)
        {
            //Arrange
            _genreRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();
            _genreRepository.Setup(x => x.Delete(It.IsAny<Genre>()));

            var genreService = new GenreService(_genreRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            genreService.Invoking(x => x.GetAsync(id)).Should().ThrowAsync<Exception>();
        }
    }
}
