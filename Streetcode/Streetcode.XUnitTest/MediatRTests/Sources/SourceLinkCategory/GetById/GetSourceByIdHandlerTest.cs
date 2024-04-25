// <copyright file="GetSourceByIdHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.Sources.GetById
{
    using Ardalis.Specification;
    using AutoMapper;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
    using Moq;
    using Streetcode.BLL.Dto.Media.Images;
    using Streetcode.BLL.Dto.Sources;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Sources;
    using Streetcode.BLL.MediatR.Sources.SourceLink.GetCategoryById;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Entities.Sources;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Specification.Sources.SourceLinkCategory;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// Can not test.
    /// </summary>
    public class GetSourceByIdHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly IMapper _mapper;
        private readonly Mock<IBlobService> _mockBlob;
        private readonly Mock<ILoggerService> _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSourceByIdHandlerTest"/> class.
        /// </summary>
        public GetSourceByIdHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetSourceRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<SourceLinkCategoryProfile>();
                c.CreateMap<SourceLinkCategory, SourceLinkCategoryDto>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image != null ? new ImageDto { Base64 = src.Image.Base64 } : null))
            .ReverseMap()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image != null ? new Image { Base64 = src.Image.Base64 } : null));

                c.CreateMap<Image, ImageDto>().ReverseMap();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();

            _mockBlob = new Mock<IBlobService>();
        }

        /// <summary>
        /// Get by id not null test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetByIdNotNullTest()
        {
            // Arrange
            SetupRepository();
            var handler = new GetCategoryByIdHandler(_mockRepository.Object, _mapper, _mockBlob.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetCategoryByIdQuery(1), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }

        /// <summary>
        /// Get by id first item title type should be "First title".
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetByIdFirstShouldBeFirstTest()
        {
            // Arrange
            SetupRepository();
            string base64 = "base64Test";
            _mockBlob.Setup(x => x.FindFileInStorageAsBase64(It.IsAny<string>())).Returns(base64);

            var handler = new GetCategoryByIdHandler(_mockRepository.Object, _mapper, _mockBlob.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetCategoryByIdQuery(1), CancellationToken.None);

            // Assert
            result.Value.Title.Should().Be("First title");
        }

        /// <summary>
        /// Get by id second item title should not be fourth item title.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetByIdSecondShouldNotBeFourthTest()
        {
            // Arrange
            SetupRepository();
            var handler = new GetCategoryByIdHandler(_mockRepository.Object, _mapper, _mockBlob.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetCategoryByIdQuery(2), CancellationToken.None);

            // Assert
            result.Value.Title.Should().NotBe("Fourth title");
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

            var sources = new List<SourceLinkCategory>()
            {
                new SourceLinkCategory { Id = 1, Title = "First title", ImageId = 1, Image = images[0], Streetcodes = streetcodeContents },
                new SourceLinkCategory { Id = 2, Title = "Second title", ImageId = 2, Image = images[1], Streetcodes = streetcodeContents },
                new SourceLinkCategory { Id = 3, Title = "Third title", ImageId = 3, Image = images[2], Streetcodes = streetcodeContents },
                new SourceLinkCategory { Id = 4, Title = "Fourth title", ImageId = 4, Image = images[3] },
            };

            var streetcodeCategoryContents = new List<StreetcodeCategoryContent>()
            {
                new StreetcodeCategoryContent() { SourceLinkCategoryId = 1, StreetcodeId = 1, Text = "Test1" },
                new StreetcodeCategoryContent() { SourceLinkCategoryId = 2, StreetcodeId = 1, Text = "Test2" },
            };

            _mockRepository.Setup(repo => repo.SourceCategoryRepository.GetItemBySpecAsync(
            It.IsAny<ISpecification<SourceLinkCategory>>()))
            .ReturnsAsync((GetByIdSourceLinkCategoryIncludeSpec spec) =>
            {
                int id = spec.Id;

                var category = sources.FirstOrDefault(s => s.Id == id);

                return category;
            });
        }
    }
}
