namespace Streetcode.XUnitTest.Mocks;

using Ardalis.Specification;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using Moq;
using Streetcode.DAL.Entities.Sources;
using Streetcode.DAL.Entities.Team;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Sources.SourceLinkCategory;
using Streetcode.DAL.Specification.Team.Positions;
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

        mockRepo.Setup(repo => repo.PositionRepository.GetItemsBySpecAsync(
        It.IsAny<ISpecification<Positions>>()))
        .ReturnsAsync((GetAllPositionsSpec spec) =>
        {
            return positions;
        });

        return mockRepo;
    }
}
