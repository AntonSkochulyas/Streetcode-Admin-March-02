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
    }
}
