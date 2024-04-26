namespace Streetcode.XUnitTest.MediatRTests.AdditionalContent.Tag.GetTagByTitle
{
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.AdditionalContent;
    using Streetcode.BLL.MediatR.AdditionalContent.Tag.GetByStreetcodeId;
    using Streetcode.BLL.MediatR.AdditionalContent.Tag.GetTagByTitle;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    public class GetTagByTitleHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        public GetTagByTitleHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetTagRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<TagProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        [Fact]
        public async Task Handler_GetTagByValidTitle_GetByIdResultShouldBeNotNull()
        {
            // Arrange
            var handler = new GetTagByTitleHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            string validTitle = "Test";
            var request = new GetTagByTitleQuery(validTitle);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task Handler_GetTagByInvalidTitle_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new GetTagByTitleHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            string invalidTitle = "Title doesn't exist";
            var request = new GetTagByTitleQuery(invalidTitle);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }
    }
}
