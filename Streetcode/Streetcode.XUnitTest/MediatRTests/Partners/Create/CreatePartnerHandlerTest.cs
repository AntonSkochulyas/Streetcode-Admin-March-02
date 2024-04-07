// <copyright file="CreatePartnerHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.Partners.Create
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.Partners;
    using Streetcode.BLL.Dto.Streetcode;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Partners;
    using Streetcode.BLL.MediatR.Partners.Create;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    ///  Can not test.
    /// </summary>
    public class CreatePartnerHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly IMapper _mapper;
        private readonly Mock<ILoggerService> _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePartnerHandlerTest"/> class.
        /// </summary>
        public CreatePartnerHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetPartnersRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<PartnerProfile>();
                c.CreateMap<StreetcodeShortDto, StreetcodeContent>().ReverseMap();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        /// <summary>
        /// Create not null and should be created test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task CreateNotNullPartnerMustBeCreated()
        {
            // Arrange
            var handler = new CreatePartnerHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(
                new CreatePartnerCommand(new CreatePartnerDto()
                {
                    Id = 1,
                    IsKeyPartner = true,
                    IsVisibleEverywhere = true,
                    Title = "Created title",
                    Description = "Created description",
                    TargetUrl = "Created target url",
                    LogoId = 1,
                    UrlTitle = "Created url title",
                }), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }
    }
}
