using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.AdditionalContent.Coordinates.Types;
using Streetcode.BLL.Dto.News;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Sources;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Update;
using Streetcode.BLL.MediatR.Newss.Update;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Update;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Sources.SourceLinkCategory.Update
{
    public class UpdateStreetcodeCategoryContentHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        public UpdateStreetcodeCategoryContentHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetSourceRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<SourceLinkCategoryProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        [Fact]
        public async Task Handler_SourceLinkCategoryDtoIsNull_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateSourceLinkCategoryHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            SourceLinkCategoryDto? sourceLinkCategoryContentDto = null;
            var request = new UpdateSourceLinkCategoryCommand(sourceLinkCategoryContentDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_ValidDto_IsSucessShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateSourceLinkCategoryHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            var sourceLinkCategoryContentDto = new SourceLinkCategoryDto()
            {
                ImageId = 1,
                Id = 1,
                Title = "Test",
            };
            var request = new UpdateSourceLinkCategoryCommand(sourceLinkCategoryContentDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_ValidDto_ResultShouldBeOfTypeSourceLinkCategoryDto()
        {
            // Arrange
            var handler = new UpdateSourceLinkCategoryHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            var sourceLinkCategoryContentDto = new SourceLinkCategoryDto()
            {
                ImageId = 1,
                Id = 1,
                Title = "Test",
            };
            var request = new UpdateSourceLinkCategoryCommand(sourceLinkCategoryContentDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().BeOfType<SourceLinkCategoryDto>();
        }
    }
}
