namespace Streetcode.XUnitTest.Mocks;

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
            new TeamMember { Id = 1, FirstName = "John", LastName = "Doe", Description = "description1", IsMain = true, ImageId = 1 },
            new TeamMember { Id = 2, FirstName = "Jane", LastName = "Mur", Description = "description2", IsMain = true, ImageId = 2 },
            new TeamMember { Id = 3, FirstName = "Mila", LastName = "Lyubow", Description = "description3", IsMain = false, ImageId = 3 },
            new TeamMember { Id = 4, FirstName = "Orest", LastName = "Fifa", Description = "description4", IsMain = false, ImageId = 2 },
            new TeamMember { Id = 5, FirstName = "Alex", LastName = "Smith", Description = "description5", IsMain = false, ImageId = 4 },
            new TeamMember { Id = 6, FirstName = "Emily", LastName = "Johnson", Description = "description6", IsMain = false, ImageId = 5 }
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

        mockRepo.Setup(x => x.TeamRepository.Create(It.IsAny<TeamMember>()))
             .Returns((TeamMember teamMember) =>
             {
                 members.Add(teamMember);
                 return teamMember;
             });

        mockRepo.Setup(x => x.TeamRepository.Delete(It.IsAny<TeamMember>()))
        .Callback((TeamMember teamMember) =>
        {
            members.Remove(teamMember);
        });

        return mockRepo;
    }
}
