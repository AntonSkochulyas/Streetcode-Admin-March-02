namespace Streetcode.XUnitTest.MediatRTests.AdditionalContent.Coordinate.Update
{
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.AdditionalContent.Coordinates.Types;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.AdditionalContent.Coordinates;
    using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Update;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    public class UpdateCoordinateHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        public UpdateCoordinateHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetCoordinateRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<StreetcodeCoordinateProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        [Fact]
        public async Task Handler_CoordinateDTOIsNull_IsFaildeShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateCoordinateHandler(_mockRepository.Object, _mapper);
            StreetcodeCoordinateDto? streetcodeCoordinateDTO = null;
            var streetcodeCoordinate = new UpdateCoordinateCommand(streetcodeCoordinateDTO);

            // Act
            var result = await handler.Handle(streetcodeCoordinate, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_ValidData_IsSucessShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateCoordinateHandler(_mockRepository.Object, _mapper);
            var streetcodeCoordinateDTO = new StreetcodeCoordinateDto()
            {
                StreetcodeId = 1,
                Id = 1,
                Latitude = 1,
                Longtitude = 1,
            };
            var streetcodeCoordinate = new UpdateCoordinateCommand(streetcodeCoordinateDTO);

            // Act
            var result = await handler.Handle(streetcodeCoordinate, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
