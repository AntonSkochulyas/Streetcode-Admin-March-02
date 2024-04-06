// <copyright file="CreateAudioHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.Media.Audio.Create
{
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.Media.Audio;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Media;
    using Streetcode.BLL.MediatR.Media.Audio.Create;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// Can not test.
    /// </summary>
    public class CreateAudioHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _mockBlob;


        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAudioHandlerTest"/> class.
        /// </summary>
        public CreateAudioHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetAudiosRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<AudioProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();

            _mockBlob = new Mock<IBlobService>();
        }


        /// <summary>
        /// Create not null and should be created test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task CreateNotNullAudioMustBeCreated()
        {
            // Arrange
            var handler = new CreateAudioHandler(_mockBlob.Object, _mockRepository.Object, _mapper,  _mockLogger.Object);

            // Act
            var result = await handler.Handle(
                new CreateAudioCommand(new AudioFileBaseCreateDto()
            {
                Description = "Created audio",
                Title = "Created title",
                Extension = ".txt",
                BaseFormat = "Created base",
                MimeType = "Created mime type",
            }), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }
    }
}