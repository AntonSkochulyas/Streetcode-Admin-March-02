namespace Streetcode.XUnitTest.MediatRTests.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Team;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    public static Mock<IRepositoryWrapper> GetTeamPositionRepositoryMock()
    {
        var positions = new List<Positions>
            {
                new Positions(),
                new Positions(),
                new Positions(),
            };

        var mockRepo = new Mock<IRepositoryWrapper>();

        mockRepo.Setup(x => x.PositionRepository
            .GetAllAsync(
                It.IsAny<Expression<Func<Positions, bool>>>(),
                It.IsAny<Func<IQueryable<Positions>, IIncludableQueryable<Positions, object>>>()))
            .ReturnsAsync(positions);

        return mockRepo;
    }
}
