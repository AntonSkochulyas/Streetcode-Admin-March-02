using Ardalis.Specification;
using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using Moq;
using Streetcode.BLL.Dto.Media.Images;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Sources;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.GetAll;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Sources;
using Streetcode.DAL.Specification.Sources.SourceLinkCategory;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Sources.SourceLinkCategory.GetAll
{
    public class GetAllSourceLinkCategoriesHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly IMapper _mapper;
        private readonly Mock<IBlobService> _mockBlob;
        private readonly Mock<ILoggerService> _mockLogger;

        public GetAllSourceLinkCategoriesHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetSourceRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<SourceLinkCategoryProfile>();
                c.AddProfile<SourceLinkSubCategoryProfile>();
                c.AddProfile<StreetcodeCategoryContentProfile>();
                c.CreateMap<Image, ImageDto>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();

            _mockBlob = new Mock<IBlobService>();
        }

        [Fact]
        public async Task GetAllNotNullTest()
        {
            // Arrange
            SetupRepository();
            var handler = new GetAllCategoriesHandler(_mockRepository.Object, _mapper, _mockBlob.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetAllCategoriesQuery(), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAllSecondBlobNameShouldBeSpecific()
        {
            // Arrange
            SetupRepository();
            var handler = new GetAllCategoriesHandler(_mockRepository.Object, _mapper, _mockBlob.Object, _mockLogger.Object);
            string expected = "Second title";

            // Act
            var result = await handler.Handle(new GetAllCategoriesQuery(), CancellationToken.None);

            // Assert
            result.Value.ElementAt(1).Title.Should().Be(expected);
        }

        private void SetupRepository()
        {
            var images = new List<Image>()
        {
            new Image() { Id = 1, Base64 = "TestBlob1" },
            new Image() { Id = 2, Base64 = "TestBlob2" },
            new Image() { Id = 3, Base64 = "TestBlob3" },
            new Image() { Id = 4, Base64 = "TestBlob4" }
        };

            var streetcodeContents = new List<StreetcodeContent>()
        {
           new StreetcodeContent()
           {
               Id = 1,
           },
           new StreetcodeContent()
           {
               Id = 1,
           },
        };

            var sources = new List<DAL.Entities.Sources.SourceLinkCategory>()
        {
            new DAL.Entities.Sources.SourceLinkCategory { Id = 1, Title = "First title", ImageId = 1, Image = images[0], Streetcodes = streetcodeContents },
            new DAL.Entities.Sources.SourceLinkCategory { Id = 2, Title = "Second title", ImageId = 2, Image = images[1], Streetcodes = streetcodeContents },
            new DAL.Entities.Sources.SourceLinkCategory { Id = 3, Title = "Third title", ImageId = 3, Image = images[2], Streetcodes = streetcodeContents },
            new DAL.Entities.Sources.SourceLinkCategory { Id = 4, Title = "Fourth title", ImageId = 4, Image = images[3] },
        };

            _mockRepository.Setup(repo => repo.SourceCategoryRepository.GetItemsBySpecAsync(
        It.IsAny<ISpecification<DAL.Entities.Sources.SourceLinkCategory>>()))
        .ReturnsAsync((GetAllSourceLinkCategoryIncludeSpec spec) =>
        {
            return sources;
        });
        }
    }
}
