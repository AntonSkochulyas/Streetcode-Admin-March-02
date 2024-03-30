namespace Streetcode.XUnitTest.MediatRTests.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Entities.Sources;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    public static Mock<IRepositoryWrapper> GetSourceRepositoryMock()
    {
        var sources = new List<SourceLinkCategory>()
            {
                new SourceLinkCategory { Id = 1, Title = "First title", ImageId = 1, Image = new Image() { Id = 1, Base64 = "Test1", } },
                new SourceLinkCategory { Id = 2, Title = "Second title", ImageId = 2, Image = new Image() { Id = 2, Base64 = "Test2", } },
                new SourceLinkCategory { Id = 3, Title = "Third title", ImageId = 3, Image = new Image() { Id = 3, Base64 = "Test3", } },
                new SourceLinkCategory { Id = 4, Title = "Fourth title", ImageId = 4, Image = new Image() { Id = 4, Base64 = "Test4", } },
            };

        var mockRepo = new Mock<IRepositoryWrapper>();

        mockRepo.Setup(x => x.SourceCategoryRepository.GetAllAsync(It.IsAny<Expression<Func<SourceLinkCategory, bool>>>(), It.IsAny<Func<IQueryable<SourceLinkCategory>, IIncludableQueryable<SourceLinkCategory, object>>>()))
            .ReturnsAsync(sources);

        mockRepo.Setup(repo => repo.SourceCategoryRepository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<SourceLinkCategory, bool>>>(),
            It.IsAny<Func<IQueryable<SourceLinkCategory>,
            IIncludableQueryable<SourceLinkCategory, object>>>()))
            .ReturnsAsync(
            (
                Expression<Func<SourceLinkCategory, bool>> predicate,
                Func<IQueryable<SourceLinkCategory>,
                IIncludableQueryable<SourceLinkCategory, object>> include) =>
            {
                return sources.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.SourceCategoryRepository.Create(It.IsAny<SourceLinkCategory>())).Returns(sources[0]);
        mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        return mockRepo;
    }
}
