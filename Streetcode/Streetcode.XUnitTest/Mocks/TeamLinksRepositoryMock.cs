namespace Streetcode.XUnitTest.Mocks;

using Ardalis.Specification;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Team;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Team.Positions;
using Streetcode.DAL.Specification.Team.TeamMemberLinks;
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

        mockRepo.Setup(repo => repo.TeamLinkRepository.GetItemsBySpecAsync(
        It.IsAny<ISpecification<TeamMemberLink>>()))
        .ReturnsAsync((GetAllTeamMemberLinksSpec spec) =>
        {
            return links;
        });


        return mockRepo;
    }
}
