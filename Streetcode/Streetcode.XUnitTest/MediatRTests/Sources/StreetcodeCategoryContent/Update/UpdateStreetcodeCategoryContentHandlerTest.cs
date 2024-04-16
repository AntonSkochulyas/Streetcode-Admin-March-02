using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Sources;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Update;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Update;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Sources.StreetcodeCategoryContent.Update
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
                c.AddProfile<SourceLinkSubCategoryProfile>();
                c.AddProfile<StreetcodeCategoryContentProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        [Fact]
        public async Task Handler_StreetcodeCategoryContentDtoIsNull_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateStreetcodeCategoryContentHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            StreetcodeCategoryContentDto? sourceLinkCategoryDto = null;
            var request = new UpdateStreetcodeCategoryContentCommand(sourceLinkCategoryDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_StreetcodeCategoryContentValidDto_IsSucessShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateStreetcodeCategoryContentHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            var streetCategoryContentDto = new StreetcodeCategoryContentDto()
            {
                SourceLinkCategoryId = 1,
                StreetcodeId = 1,
                Text = "Test",
            };

            var request = new UpdateStreetcodeCategoryContentCommand(streetCategoryContentDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_StreetcodeCategoryContentValidDto_ResultShouldBeOfTypeStreetcodeCategoryContentDto()
        {
            // Arrange
            var handler = new UpdateStreetcodeCategoryContentHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            var streetCategoryContentDto = new StreetcodeCategoryContentDto()
            {
                SourceLinkCategoryId = 1,
                StreetcodeId = 1,
                Text = "Test",
            };

            var request = new UpdateStreetcodeCategoryContentCommand(streetCategoryContentDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().BeOfType<StreetcodeCategoryContentDto>();
        }
    }
}
