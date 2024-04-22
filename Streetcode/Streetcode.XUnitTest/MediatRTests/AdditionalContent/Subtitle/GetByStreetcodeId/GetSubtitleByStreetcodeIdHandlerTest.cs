namespace Streetcode.XUnitTest.MediatRTests.AdditionalContent.Subtitle.GetByStreetcodeId
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Castle.Core.Logging;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.AdditionalContent;
    using Streetcode.BLL.MediatR.AdditionalContent.Subtitle.GetByStreetcodeId;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    public class GetSubtitleByStreetcodeIdHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _logger;

        public GetSubtitleByStreetcodeIdHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetSubtitleRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<SubtitleProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _logger = new Mock<ILoggerService>();
        }

        [Fact]
        public async Task Handler_GetByStreetcodeValidId_ResultShouldNotBeNull()
        {
            // Arrange
            var handler = new GetSubtitlesByStreetcodeIdHandler(_mockRepository.Object, _mapper, _logger.Object);
            int validId = 1;
            var request = new GetSubtitlesByStreetcodeIdQuery(validId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task Handler_GetByStreetcodeInvalidId_ResultShouldBeNull()
        {
            // Arrange
            var handler = new GetSubtitlesByStreetcodeIdHandler(_mockRepository.Object, _mapper, _logger.Object);
            int invalidId = 10;
            var request = new GetSubtitlesByStreetcodeIdQuery(invalidId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }
    }
}
