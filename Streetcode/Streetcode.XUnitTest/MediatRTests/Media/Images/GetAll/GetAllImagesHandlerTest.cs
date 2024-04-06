// <copyright file="GetAllImagesHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.Media.Images
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.Media.Audio;
    using Streetcode.BLL.Dto.Media.Images;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Media.Audio.GetAll;
    using Streetcode.BLL.MediatR.Media.Image.GetAll;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// TESTED SUCCESSFULLY.
    /// </summary>
    public class GetAllImagesHandlerTest
    {
        private readonly Mock<IRepositoryWrapper>? _mockRepository;
        private readonly Mock<IMapper>? _mockMapper;
        private readonly Mock<IBlobService>? _mockBlobService;
        private readonly Mock<ILoggerService>? _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllImagesHandlerTest"/> class.
        /// </summary>
        public GetAllImagesHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetImagesRepositoryMock();
            _mockMapper = new Mock<IMapper>();
            _mockBlobService = new Mock<IBlobService>();
            _mockLogger = new Mock<ILoggerService>();

            ConfigureMapper(GetImagesDTOList());
        }

        /// <summary>
        /// Get all not null or empty test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetAllNotNullOrEmptyTest()
        {
            // Arrange
            var handler = new GetAllImagesHandler(_mockRepository.Object, _mockMapper.Object, _mockBlobService.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetAllImagesQuery(), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNullOrEmpty();
        }

        /// <summary>
        /// Get all list count should be two.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetAllCountShouldBeTwo()
        {
            // Arrange
            var handler = new GetAllImagesHandler(_mockRepository.Object, _mockMapper.Object, _mockBlobService.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetAllImagesQuery(), CancellationToken.None);

            // Assert
            result.Value.Count().Should().Be(2);
        }

        /// <summary>
        /// Get all list should be type AudioDTO.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetAllShouldBeTypeListAudioDTO()
        {
            // Arrange
            var handler = new GetAllImagesHandler(_mockRepository.Object, _mockMapper.Object, _mockBlobService.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetAllImagesQuery(), CancellationToken.None);

            // Assert
            result.Value.Should().BeOfType<List<ImageDto>>();
        }

        private List<ImageDto> GetImagesDTOList()
        {
            return new List<ImageDto>()
            {
                new ImageDto
                {
                    Id = 1,
                },
                new ImageDto
                {
                    Id = 2,
                },
            };
        }

        private void ConfigureMapper(List<ImageDto> imageListDTO)
        {
            _mockMapper?.Setup(x => x.Map<IEnumerable<ImageDto>>(It.IsAny<IEnumerable<object>>()))
            .Returns(imageListDTO);
        }
    }
}