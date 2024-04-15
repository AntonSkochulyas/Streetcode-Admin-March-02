namespace Streetcode.XUnitTest.MediatRTests.Instagram.GetAll
{
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.Instagram;
    using Streetcode.BLL.MediatR.Instagram.GetAll;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    public class GetAllPostsHandlerTest
    {
        private readonly Mock<IInstagramService> _mockRepository;

        public GetAllPostsHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetInstagramPostsMock();
        }

        [Fact]
        public async Task Get_All_Not_Null_Or_Empty_Test()
        {
            var handler = new GetAllPostsHandler(_mockRepository.Object);

            var result = await handler.Handle(new GetAllPostsQuery(), CancellationToken.None);

            result.Value.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_All_Count_Should_Be_Four()
        {
            var handler = new GetAllPostsHandler(_mockRepository.Object);

            var result = await handler.Handle(new GetAllPostsQuery(), CancellationToken.None);

            result.Value.Count().Should().Be(4);
        }
    }
}