namespace Streetcode.XUnitTest.MediatRTests.AdditionalContent.Tag.GetById
{
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.AdditionalContent;
    using Streetcode.BLL.MediatR.AdditionalContent.Tag.GetById;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    public class GetTagByIdHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        public GetTagByIdHandlerTest()
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
        public async Task Handler_GetTagByValidId_GetByIdResultShouldBeNotNull()
        {
            // Arrange
            var handler = new GetTagByIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            int validId = 1;
            var request = new GetTagByIdQuery(validId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task Handler_GetTagByInvalidId_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new GetTagByIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            int invalidId = 10;
            var request = new GetTagByIdQuery(invalidId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }
    }
}
