// <copyright file="CreateLocationHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.Locations.Create
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Locations;
    using Streetcode.BLL.MediatR.Locations.Create;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    ///  Can not test.
    /// </summary>
    public class CreateLocationHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly IMapper _mapper;
        private readonly Mock<ILoggerService> _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateLocationHandlerTest"/> class.
        /// </summary>
        public CreateLocationHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetLocationRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LocationProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        /// <summary>
        /// Create not null and should be created test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task CreateNotNullLocationMustBeCreated()
        {
            // Arrange
            var handler = new CreateLocationHandler(_mapper, _mockRepository.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(
                new CreateLocationCommand(new BLL.Dto.Locations.LocationDto()
                {
                    Id = 1,
                    Streetname = "Created StreetName",
                    TableNumber = 1,
                }), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }
    }
}
