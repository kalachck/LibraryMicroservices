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
    public class GenreServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IDbManager<Genre>> _dbManager;
        private readonly Mock<IBaseRepository<Genre, ApplicationContext>> _genreRepository;
        private readonly Fixture _fixture;

        public GenreServiceTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GenreProfile>();
                cfg.AddProfile<BookProfile>();
            });

            _mapper = new Mapper(configuration);
            _dbManager = new Mock<IDbManager<Genre>>();
            _genreRepository = new Mock<IBaseRepository<Genre, ApplicationContext>>();
            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _fixture.Customize<Genre>(x => x.Without(p => p.Books));
            _fixture.Customize<GenreDTO>(x => x.Without(p => p.Books));
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldReturnGenreDTO(int id)
        {
            //Arrange
            _genreRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Genre>());

            var genreService = new GenreService(_genreRepository.Object, _dbManager.Object, _mapper);

            //Act
            var actualResult = await genreService.GetAsync(id);

            //Assert
            actualResult.Should().BeOfType<GenreDTO>();
            actualResult.Name.Should().NotBeNull();
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldThrowNotFoundException(int id)
        {
            //Arrange
            _genreRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult((Genre)null));

            var genreService = new GenreService(_genreRepository.Object, _dbManager.Object, _mapper);

            //Act
            var act = () => genreService.GetAsync(id);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>("Record was not found");
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldThrowException(int id)
        {
            //Arrange
            _genreRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();

            var genreService = new GenreService(_genreRepository.Object, _dbManager.Object, _mapper);

            //Act
            var act = () => genreService.GetAsync(id);

            //Assert
            await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task AddAsync_ShouldReturnSuccessfullMessage()
        {
            //Arrange
            _genreRepository.Setup(x => x.Add(It.IsAny<Genre>()));

            var genreService = new GenreService(_genreRepository.Object, _dbManager.Object, _mapper);

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

            var genreService = new GenreService(_genreRepository.Object, _dbManager.Object, _mapper);

            //Act
            var act = () => genreService.AddAsync(_fixture.Create<GenreDTO>());

            //Assert
            await act.Should().ThrowAsync<Exception>();
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldReturnSuccessfullMessage(int id)
        {
            //Arrange
            _genreRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Genre>());
            _genreRepository.Setup(x => x.Update(It.IsAny<Genre>()));

            var genreService = new GenreService(_genreRepository.Object, _dbManager.Object, _mapper);

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

            var genreService = new GenreService(_genreRepository.Object, _dbManager.Object, _mapper);

            //Act
            var act = () => genreService.UpdateAsync(id, _fixture.Create<GenreDTO>());

            //Assert
            await act.Should().ThrowAsync<NotFoundException>("Record was not found");
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldThrowException(int id)
        {
            //Arrange
            _genreRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();
            _genreRepository.Setup(x => x.Update(It.IsAny<Genre>()));

            var genreService = new GenreService(_genreRepository.Object, _dbManager.Object, _mapper);

            //Act
            var act = () => genreService.UpdateAsync(id, _fixture.Create<GenreDTO>());

            //Assert
            await act.Should().ThrowAsync<Exception>();
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldReturnSuccessfullMessage(int id)
        {
            //Arrange
            _genreRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Genre>());
            _genreRepository.Setup(x => x.Delete(It.IsAny<Genre>()));

            var genreService = new GenreService(_genreRepository.Object, _dbManager.Object, _mapper);

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

            var genreService = new GenreService(_genreRepository.Object, _dbManager.Object, _mapper);

            //Act
            var act = () => genreService.DeleteAsync(id);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>("Record was not found");
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldThrowException(int id)
        {
            //Arrange
            _genreRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();
            _genreRepository.Setup(x => x.Delete(It.IsAny<Genre>()));

            var genreService = new GenreService(_genreRepository.Object, _dbManager.Object, _mapper);

            //Act
            var act = () => genreService.DeleteAsync(id);

            //Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
