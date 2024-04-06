namespace Streetcode.XUnitTest.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Team;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    public static Mock<IRepositoryWrapper> GetTeamLinksRepositoryMock()
    {
        var links = new List<TeamMemberLink>
            {
                new TeamMemberLink(),
                new TeamMemberLink(),
                new TeamMemberLink(),
                new TeamMemberLink(),
            };

        var mockRepo = new Mock<IRepositoryWrapper>();

        mockRepo.Setup(x => x.TeamLinkRepository
            .GetAllAsync(
                It.IsAny<Expression<Func<TeamMemberLink, bool>>>(),
                It.IsAny<Func<IQueryable<TeamMemberLink>, IIncludableQueryable<TeamMemberLink, object>>>()))
            .ReturnsAsync(links);

        return mockRepo;
    }
}
