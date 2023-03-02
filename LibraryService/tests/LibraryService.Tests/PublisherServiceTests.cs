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
    public class PublisherServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IDbManager<Publisher>> _dbManager;
        private readonly Mock<IBaseRepository<Publisher, ApplicationContext>> _publisherRepository;
        private readonly Fixture _fixture;

        public PublisherServiceTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PublisherProfile>();
            });

            _mapper = new Mapper(configuration);
            _dbManager = new Mock<IDbManager<Publisher>>();
            _publisherRepository = new Mock<IBaseRepository<Publisher, ApplicationContext>>();
            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _fixture.Customize<Publisher>(x => x.Without(p => p.Books));
            _fixture.Customize<PublisherDTO>(x => x.Without(p => p.Books));
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldReturnPublisherDTO(int id)
        {
            //Arrange
            _publisherRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Publisher>());

            var publisherService = new PublisherService(_publisherRepository.Object, _dbManager.Object, _mapper);

            //Act
            var actualResult = await publisherService.GetAsync(id);

            //Assert
            actualResult.Should().BeOfType<PublisherDTO>();
            actualResult.Name.Should().NotBeNull();
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldThrowNotFoundException(int id)
        {
            //Arrange
            _publisherRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult((Publisher)null));

            var publisherService = new PublisherService(_publisherRepository.Object, _dbManager.Object, _mapper);

            //Act
            var act = () => publisherService.GetAsync(id);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>("Record was not found");
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldThrowException(int id)
        {
            //Arrange
            _publisherRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();

            var publisherService = new PublisherService(_publisherRepository.Object, _dbManager.Object, _mapper);

            //Act
            var act = () => publisherService.GetAsync(id);

            //Assert
            await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task AddAsync_ShouldReturnSuccessfullMessage()
        {
            //Arrange
            _publisherRepository.Setup(x => x.Add(It.IsAny<Publisher>()));

            var publisherService = new PublisherService(_publisherRepository.Object, _dbManager.Object, _mapper);

            //Act
            var actualResult = await publisherService.AddAsync(_fixture.Create<PublisherDTO>());

            //Assert
            actualResult.Should().BeOfType<string>("The record was successfully added");
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException()
        {
            //Arrange
            _publisherRepository.Setup(x => x.Add(It.IsAny<Publisher>())).Throws<Exception>();

            var publisherService = new PublisherService(_publisherRepository.Object, _dbManager.Object, _mapper);

            //Act
            var act = () => publisherService.AddAsync(_fixture.Create<PublisherDTO>());

            //Assert
            await act.Should().ThrowAsync<Exception>();
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldReturnSuccessfullMessage(int id)
        {
            //Arrange
            _publisherRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Publisher>());
            _publisherRepository.Setup(x => x.Update(It.IsAny<Publisher>()));

            var publisherService = new PublisherService(_publisherRepository.Object, _dbManager.Object, _mapper);

            //Act
            var actualResult = await publisherService.UpdateAsync(id, _fixture.Create<PublisherDTO>());

            //Assert
            actualResult.Should().BeOfType<string>("The record was successfully updated");
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldReturnNotFoundException(int id)
        {
            //Arrange
            _publisherRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync((Publisher)null);
            _publisherRepository.Setup(x => x.Update(It.IsAny<Publisher>()));

            var publisherService = new PublisherService(_publisherRepository.Object, _dbManager.Object, _mapper);

            //Act
            var act = () => publisherService.UpdateAsync(id, _fixture.Create<PublisherDTO>());

            //Assert
            await act.Should().ThrowAsync<NotFoundException>("Record was not found");
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldThrowException(int id)
        {
            //Arrange
            _publisherRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();
            _publisherRepository.Setup(x => x.Update(It.IsAny<Publisher>()));

            var publisherService = new PublisherService(_publisherRepository.Object, _dbManager.Object, _mapper);

            //Act
            var act = () => publisherService.UpdateAsync(id, _fixture.Create<PublisherDTO>());

            //Assert
            await act.Should().ThrowAsync<Exception>();
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldReturnSuccessfullMessage(int id)
        {
            //Arrange
            _publisherRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Publisher>());
            _publisherRepository.Setup(x => x.Delete(It.IsAny<Publisher>()));

            var genreService = new PublisherService(_publisherRepository.Object, _dbManager.Object, _mapper);

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
            _publisherRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult((Publisher)null));
            _publisherRepository.Setup(x => x.Delete(It.IsAny<Publisher>()));

            var publisherService = new PublisherService(_publisherRepository.Object, _dbManager.Object, _mapper);

            //Act
            var act = () => publisherService.DeleteAsync(id);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>("Record was not found");
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldThrowException(int id)
        {
            //Arrange
            _publisherRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();
            _publisherRepository.Setup(x => x.Delete(It.IsAny<Publisher>()));

            var publisherService = new PublisherService(_publisherRepository.Object, _dbManager.Object, _mapper);

            //Act
            var act = () => publisherService.DeleteAsync(id);

            //Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
