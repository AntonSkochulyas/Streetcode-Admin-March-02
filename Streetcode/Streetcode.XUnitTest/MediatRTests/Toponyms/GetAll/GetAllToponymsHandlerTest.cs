// <copyright file="GetAllToponymsHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.Toponyms.GetAll
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.Toponyms;
    using Streetcode.BLL.Mapping.Toponyms;
    using Streetcode.BLL.MediatR.Toponyms.GetAll;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// Can not test.
    /// </summary>
    public class GetAllToponymsHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllToponymsHandlerTest"/> class.
        /// </summary>
        public GetAllToponymsHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetToponymsRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ToponymProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        /// <summary>
        /// Get all not null or empty test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetAllNotNullOrEmptyTest()
        {
            // Arrange
            var handler = new GetAllToponymsHandler(_mockRepository.Object, _mapper);

            // Act
            var result = await handler.Handle(new GetAllToponymsQuery(new GetAllToponymsRequestDto() { Title = "First streetname" }), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }

        /// <summary>
        /// Get all list should be type ArtDTO.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetAllShouldBeTypeListToponymDTO()
        {
            // Arrange
            var handler = new GetAllToponymsHandler(_mockRepository.Object, _mapper);

            // Act
            var result = await handler.Handle(new GetAllToponymsQuery(new GetAllToponymsRequestDto() { Title = "First streetname" }), CancellationToken.None);

            // Assert
            result.Value.Toponyms.Should().BeOfType<List<ToponymDto>>();
        }

        /// <summary>
        /// Get all list should not contain unexpected items.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetAllShouldNotContainUnexpectedItems()
        {
            // Arrange
            var unexpectedTitle = "Unexpected streetname";
            var handler = new GetAllToponymsHandler(_mockRepository.Object, _mapper);

            // Act
            var result = await handler.Handle(new GetAllToponymsQuery(new GetAllToponymsRequestDto() { Title = unexpectedTitle }), CancellationToken.None);

            // Assert
            result.Value.Toponyms.Should().NotContain(dto => dto.StreetName == unexpectedTitle);
        }

        /// <summary>
        /// Get all list should contain items.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetAllShouldContainItems()
        {
            // Arrange
            var handler = new GetAllToponymsHandler(_mockRepository.Object, _mapper);

            // Act
            var result = await handler.Handle(new GetAllToponymsQuery(new GetAllToponymsRequestDto() { Title = "First streetname" }), CancellationToken.None);

            // Assert
            result.Value.Toponyms.Should().NotBeEmpty();
        }

        /// <summary>
        /// Tests the functionality of retrieving a paginated list of toponyms.
        /// </summary>
        /// <remarks>
        /// This test ensures that the GetAllWithPagination method in the GetAllToponymsHandler class
        /// correctly returns a paginated list of toponyms based on the provided query parameters.
        /// </remarks>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetAllWithPaginationTest()
        {
            // Arrange
            var handler = new GetAllToponymsHandler(_mockRepository.Object, _mapper);
            var request = new GetAllToponymsQuery(new GetAllToponymsRequestDto() { Page = 1, Amount = 10 });

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Toponyms.Should().HaveCount(4);
            result.Value.Pages.Should().Be(1);
        }

        /// <summary>
        /// Tests the behavior of retrieving toponyms with an invalid filter.
        /// </summary>
        /// <remarks>
        /// This test ensures that the GetAllWithInvalidFilter method in the GetAllToponymsHandler class
        /// returns an empty list when an invalid filter is provided in the query parameters.
        /// </remarks>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetAllWithInvalidFilterTest()
        {
            // Arrange
            var handler = new GetAllToponymsHandler(_mockRepository.Object, _mapper);
            var request = new GetAllToponymsQuery(new GetAllToponymsRequestDto() { Title = "InvalidFilter" });

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Toponyms.Should().BeEmpty();
        }

        /// <summary>
        /// Tests the behavior of retrieving toponyms with a null filter.
        /// </summary>
        /// <remarks>
        /// This test ensures that the GetAllWithNullFilter method in the GetAllToponymsHandler class
        /// returns a non-empty list when a null filter is provided in the query parameters.
        /// </remarks>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetAllWithNullFilterTest()
        {
            // Arrange
            var handler = new GetAllToponymsHandler(_mockRepository.Object, _mapper);
            var request = new GetAllToponymsQuery(new GetAllToponymsRequestDto() { Title = null });

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Toponyms.Should().NotBeEmpty();
        }
    }
}
