namespace Streetcode.XUnitTest.MediatRTests.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Team;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    public static Mock<IRepositoryWrapper> GetTeamRepositoryMock()
    {
        var members = new List<TeamMember>()
        {
            new TeamMember { Id = 1, FirstName = "1", IsMain = true },
            new TeamMember { Id = 2, FirstName = "2", IsMain = true },
            new TeamMember { Id = 3, FirstName = "3", IsMain = false },
            new TeamMember { Id = 4, FirstName = "4", IsMain = false },
            new TeamMember { Id = 5, FirstName = "5", IsMain = false },
            new TeamMember { Id = 6, FirstName = "6", IsMain = false },
        };

        var mockRepo = new Mock<IRepositoryWrapper>();

        mockRepo.Setup(x => x.TeamRepository
            .GetAllAsync(
                It.IsAny<Expression<Func<TeamMember, bool>>>(),
                It.IsAny<Func<IQueryable<TeamMember>, IIncludableQueryable<TeamMember, object>>>()))
            .ReturnsAsync(members);

        mockRepo.Setup(x => x.TeamRepository
            .GetAllAsync(
                It.IsAny<Expression<Func<TeamMember, bool>>>(),
                It.IsAny<Func<IQueryable<TeamMember>, IIncludableQueryable<TeamMember, object>>>()))
            .ReturnsAsync((
                Expression<Func<TeamMember, bool>> filter,
                Func<IQueryable<TeamMember>, IIncludableQueryable<TeamMember, object>> include) =>
            {
                IQueryable<TeamMember> query = members.AsQueryable();

                if (include != null)
                {
                    query = include(query);
                }

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                return Task.FromResult(query.ToList()).Result;
            });

        mockRepo.Setup(x => x.TeamRepository
            .GetSingleOrDefaultAsync(
                It.IsAny<Expression<Func<TeamMember, bool>>>(),
                It.IsAny<Func<IQueryable<TeamMember>, IIncludableQueryable<TeamMember, object>>>()))
            .ReturnsAsync((
                Expression<Func<TeamMember, bool>> predicate,
                Func<IQueryable<TeamMember>, IIncludableQueryable<TeamMember, object>> include) =>
            {
                var matchingMembers = members.Where(predicate.Compile()).ToList();

                if (matchingMembers.Count > 1)
                {
                    throw new InvalidOperationException("Sequence contains more than one element");
                }

                return matchingMembers.SingleOrDefault();
            });

        return mockRepo;
    }
}
