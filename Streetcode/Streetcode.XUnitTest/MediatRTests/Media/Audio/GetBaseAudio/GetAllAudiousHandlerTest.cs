using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.Media.Audio;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Media;
using Streetcode.BLL.MediatR.Media.Audio.GetAll;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Media.Audio.GetBaseAudio
{
    // TESTED SUCCESSFULLY
    // BLL -> MediatR -> Media -> Audio -> GetAll
    public class GetAllAudiousHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _mockBlob;

        public GetAllAudiousHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetAudiosRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<AudioProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();

            _mockBlob = new Mock<IBlobService>();
        }

        [Fact]
        public async Task Get_All_Not_Null_Or_Empty_Test()
        {
            // Arrange
            var handler = new GetAllAudiosHandler(_mockRepository.Object, _mapper, _mockBlob.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetAllAudiosQuery(), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_All_Count_Should_Be_Four()
        {
            // Arrange
            var handler = new GetAllAudiosHandler(_mockRepository.Object, _mapper, _mockBlob.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetAllAudiosQuery(), CancellationToken.None);

            // Assert
            result.Value.Count().Should().Be(4);
        }

        [Fact]
        public async Task Get_All_Count_Should_NotBe_Five()
        {
            // Arrange
            var handler = new GetAllAudiosHandler(_mockRepository.Object, _mapper, _mockBlob.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetAllAudiosQuery(), CancellationToken.None);

            // Assert
            result.Value.Count().Should().NotBe(5);
        }

        [Fact]
        public async Task Get_All_Should_Be_Type_List_AudioDTO()
        {
            // Arrange
            var handler = new GetAllAudiosHandler(_mockRepository.Object, _mapper, _mockBlob.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetAllAudiosQuery(), CancellationToken.None);

            // Assert
            result.Value.Should().BeOfType<List<AudioDto>>();
        }
    }
}
