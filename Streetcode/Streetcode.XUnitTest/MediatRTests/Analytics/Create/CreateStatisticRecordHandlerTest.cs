using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.Analytics;
using Streetcode.BLL.Mapping.Analytics;
using Streetcode.BLL.MediatR.Analytics.StatisticRecords.Create;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Analytics.Create
{
    public class CreateStatisticRecordHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;

        public CreateStatisticRecordHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetStatisticRecordRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<CreateStatisticRecordProfile>();
                c.AddProfile<StatisticRecordProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task CreateStatisticRecord_InputDtoIsNull_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new CreateStatisticRecordHandler(_mapper, _mockRepository.Object);
            CreateStatisticRecordDto? createStatisticDto = null;
            var request = new CreateStatisticRecordCommand(createStatisticDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task CreateStatisticRecord_QrIdIsNotUnique_IsFailedShouldBeTrue()
        {
            // Arrange
            int notUniqueQrId = 1;
            int uniqueStreetcodeId = 10;
            var handler = new CreateStatisticRecordHandler(_mapper, _mockRepository.Object);
            var createStatisticDto = new CreateStatisticRecordDto()
            {
                QrId = notUniqueQrId,
                StreetcodeCoordinateId = uniqueStreetcodeId,
                Address = "Address1"
            };
            var request = new CreateStatisticRecordCommand(createStatisticDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task CreateStatisticRecord_StreetcodeIdIsNotUnique_IsFailedShouldBeTrue()
        {
            // Arrange
            int uniqueQrId = 10;
            int notUniqueStreetcodeId = 1;
            var handler = new CreateStatisticRecordHandler(_mapper, _mockRepository.Object);
            var createStatisticDto = new CreateStatisticRecordDto()
            {
                QrId = uniqueQrId,
                StreetcodeCoordinateId = notUniqueStreetcodeId,
                Address = "Address1"
            };
            var request = new CreateStatisticRecordCommand(createStatisticDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task CreateStatisticRecord_ValidData_IsSuccessShouldBeTrue()
        {
            // Arrange
            int uniqueQrId = 10;
            int uniqueStreetcodeId = 10;
            var handler = new CreateStatisticRecordHandler(_mapper, _mockRepository.Object);
            var createStatisticDto = new CreateStatisticRecordDto()
            {
                QrId = uniqueQrId,
                StreetcodeCoordinateId = uniqueStreetcodeId,
                Address = "Address1"
            };
            var request = new CreateStatisticRecordCommand(createStatisticDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
