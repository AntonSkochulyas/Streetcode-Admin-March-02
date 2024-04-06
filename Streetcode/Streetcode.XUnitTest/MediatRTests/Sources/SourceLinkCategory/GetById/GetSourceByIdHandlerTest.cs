// <copyright file="GetSourceByIdHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.Sources.GetById
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.Media.Images;
    using Streetcode.BLL.Dto.Sources;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Sources;
    using Streetcode.BLL.MediatR.Sources.SourceLink.GetCategoryById;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Entities.Sources;
    using Streetcode.DAL.Repositories.Interfaces.Base;
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
            var handler = new GetCategoryByIdHandler(_mockRepository.Object, _mapper, _mockBlob.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetCategoryByIdQuery(2), CancellationToken.None);

            // Assert
            result.Value.Title.Should().NotBe("Fourth title");
        }
    }
}
