using AutoFixture;
using AutoMapper;
using FluentAssertions;
using LibrarySevice.Api.Mappings;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Exceptions;
using LibrarySevice.BussinesLogic.Services;
using LibrarySevice.DataAccess;
using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories.Abstract;
using Moq;

namespace LibraryService.UnitTests
{
    public class PublisherServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ApplicationContext> _context;
        private readonly Mock<IBaseRepository<Publisher, ApplicationContext>> _publisherRepository;
        private readonly Fixture _fixture;

        public PublisherServiceTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PublisherProfile>();
            });

            _mapper = new Mapper(configuration);
            _context = new Mock<ApplicationContext>();
            _publisherRepository = new Mock<IBaseRepository<Publisher, ApplicationContext>>();
            _fixture = new Fixture();

            _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldReturnPublisherDTO(int id)
        {
            //Arrange
            _publisherRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Publisher>());

            var publisherService = new PublisherService(_publisherRepository.Object, _context.Object, _mapper);

            //Act
            var actualResult = await publisherService.GetAsync(id);

            //Assert
            actualResult.Should().BeOfType<PublisherDTO>();
            actualResult.Name.Should().Be("testName");
        }

        [Theory]
        [InlineData(1)]
        public async Task GetAsync_ShouldThrowNotFoundException(int id)
        {
            //Arrange
            _publisherRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult((Publisher)null));

            var publisherService = new PublisherService(_publisherRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            publisherService.Invoking(x => x.GetAsync(id)).Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public void GetAsync_ShouldThrowException(int id)
        {
            //Arrange
            _publisherRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();

            var publisherService = new PublisherService(_publisherRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            publisherService.Invoking(x => x.GetAsync(id)).Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task AddAsync_ShouldReturnSuccessfullMessage()
        {
            //Arrange
            _publisherRepository.Setup(x => x.Add(It.IsAny<Publisher>()));

            var publisherService = new PublisherService(_publisherRepository.Object, _context.Object, _mapper);

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

            var publisherService = new PublisherService(_publisherRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            publisherService.Invoking(x => x.AddAsync(_fixture.Create<PublisherDTO>())).Should().ThrowAsync<Exception>();
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldReturnSuccessfullMessage(int id)
        {
            //Arrange
            _publisherRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Publisher>());
            _publisherRepository.Setup(x => x.Update(It.IsAny<Publisher>()));

            var publisherService = new PublisherService(_publisherRepository.Object, _context.Object, _mapper);

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

            var publisherService = new PublisherService(_publisherRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            publisherService.Invoking(x => x.UpdateAsync(id, _fixture.Create<PublisherDTO>()))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateAsync_ShouldThrowException(int id)
        {
            //Arrange
            _publisherRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();
            _publisherRepository.Setup(x => x.Update(It.IsAny<Publisher>()));

            var publisherService = new PublisherService(_publisherRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            publisherService.Invoking(x => x.UpdateAsync(id, _fixture.Create<PublisherDTO>()))
                .Should().ThrowAsync<Exception>();
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldReturnSuccessfullMessage(int id)
        {
            //Arrange
            _publisherRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(_fixture.Create<Publisher>());
            _publisherRepository.Setup(x => x.Delete(It.IsAny<Publisher>()));

            var genreService = new PublisherService(_publisherRepository.Object, _context.Object, _mapper);

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

            var publisherService = new PublisherService(_publisherRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            publisherService.Invoking(x => x.DeleteAsync(id)).Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteAsync_ShouldThrowException(int id)
        {
            //Arrange
            _publisherRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Throws<Exception>();
            _publisherRepository.Setup(x => x.Delete(It.IsAny<Publisher>()));

            var publisherService = new PublisherService(_publisherRepository.Object, _context.Object, _mapper);

            //Act
            //Assert
            publisherService.Invoking(x => x.GetAsync(id)).Should().ThrowAsync<Exception>();
        }
    }
}
