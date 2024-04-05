using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Sources;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Delete;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Delete;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.MediatRTests.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Sources.StreetcodeCategoryContent.Delete
{
    public class DeleteStreetcodeCategoryContentHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        public DeleteStreetcodeCategoryContentHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetSourceRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<SourceLinkCategoryProfile>();
                c.AddProfile<SourceLinkSubCategoryProfile>();
                c.AddProfile<StreetcodeCategoryContentProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        [Fact]
        public async Task Handler_DeleteStreetcodeCategoryContentWithWrongId_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new DeleteStreetcodeCategoryContentHandler(_mockRepository.Object, _mockLogger.Object);

            int wrongSourceLinkCategoryId = 10;
            int wrongStreetcodeId = 10;
            var request = new DeleteStreetcodeCategoryContentCommand(wrongSourceLinkCategoryId, wrongStreetcodeId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_DeleteStreetcodeCategoryContentWithCorrectId_DeleteShouldBeCalled()
        {
            // Arrange
            var handler = new DeleteStreetcodeCategoryContentHandler(_mockRepository.Object, _mockLogger.Object);

            int correctSourceLinkCategoryId = 1;
            int correctStreetcoeId = 1;
            var request = new DeleteStreetcodeCategoryContentCommand(correctSourceLinkCategoryId, correctStreetcoeId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            _mockRepository.Verify(
                x => x.StreetcodeCategoryContentRepository
                .Delete(It.IsAny<DAL.Entities.Sources.StreetcodeCategoryContent>()), Times.Once);
        }

        [Fact]
        public async Task Handler_DeleteStreetcodeCategoryContentWithCorrectId_IsSuccessShouldBeTrue()
        {
            // Arrange
            var handler = new DeleteStreetcodeCategoryContentHandler(_mockRepository.Object, _mockLogger.Object);

            int correctSourceLinkCategoryId = 1;
            int correctStreetcoeId = 1;
            var request = new DeleteStreetcodeCategoryContentCommand(correctSourceLinkCategoryId, correctStreetcoeId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
