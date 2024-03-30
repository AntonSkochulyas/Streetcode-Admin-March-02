namespace Streetcode.XUnitTest.MediatRTests.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Toponyms;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    /// <summary>
    /// Mocks toponyms repository.
    /// </summary>
    /// <returns>Returns mocked repository. </returns>
    public static Mock<IRepositoryWrapper> GetToponymsRepositoryMock()
    {
        var toponyms = new List<Toponym>
            {
               new Toponym() { Id = 1, Oblast = "First", StreetName = "First streetname" },
               new Toponym() { Id = 2, Oblast = "Second", StreetName = "Second streetname" },
               new Toponym() { Id = 3, Oblast = "Third", StreetName = "Third streetname" },
               new Toponym() { Id = 4, Oblast = "Fourth", StreetName = "Fourth streetname" },
            };

        var mockRepo = new Mock<IRepositoryWrapper>();

        mockRepo.Setup(x => x.ToponymRepository.GetAllAsync(It.IsAny<Expression<Func<Toponym, bool>>>(), It.IsAny<Func<IQueryable<Toponym>, IIncludableQueryable<Toponym, object>>>()))
            .ReturnsAsync(toponyms);

        mockRepo.Setup(x => x.ToponymRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Toponym, bool>>>(), It.IsAny<Func<IQueryable<Toponym>, IIncludableQueryable<Toponym, object>>>()))
            .ReturnsAsync((Expression<Func<Toponym, bool>> predicate, Func<IQueryable<Toponym>, IIncludableQueryable<Toponym, object>> include) =>
            {
                return toponyms.FirstOrDefault(predicate.Compile());
            });

        return mockRepo;
    }
}
