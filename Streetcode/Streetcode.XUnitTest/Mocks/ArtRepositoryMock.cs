namespace Streetcode.XUnitTest.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using Moq;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Entities.Sources;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    /// <summary>
    /// Mocks art repository.
    /// </summary>
    /// <returns>Returns mocked repository. </returns>
    public static Mock<IRepositoryWrapper> GetArtRepositoryMock()
    {
        var arts = new List<Art>()
        {
            new Art { Id = 1, Description = "First description", Title = "First title", ImageId = 1, Image = null },
            new Art { Id = 2, Description = "Second description", Title = "Second title", ImageId = 2, Image = null },
            new Art { Id = 3, Description = "Third description", Title = "First third", ImageId = 3, Image = null },
            new Art { Id = 4, Description = "Fourth description", Title = "Fourth title", ImageId = 4, Image = null },
        };

        var mockRepo = new Mock<IRepositoryWrapper>();

        mockRepo.Setup(x => x.ArtRepository.GetAllAsync(It.IsAny<Expression<Func<Art, bool>>>(), It.IsAny<Func<IQueryable<Art>, IIncludableQueryable<Art, object>>>()))
            .ReturnsAsync(arts);

        mockRepo.Setup(x => x.ArtRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Art, bool>>>(), It.IsAny<Func<IQueryable<Art>, IIncludableQueryable<Art, object>>>()))
            .ReturnsAsync((Expression<Func<Art, bool>> predicate, Func<IQueryable<Art>, IIncludableQueryable<Art, object>> include) =>
            {
                return arts.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.ArtRepository.Create(It.IsAny<Art>()))
            .Returns((Art art) =>
            {
                arts.Add(art);
                return art;
            });

        mockRepo.Setup(x => x.ArtRepository.Delete(It.IsAny<Art>()))
            .Callback((Art art) =>
            {
                art = arts.FirstOrDefault(x => x.Id == art.Id);
                if (art is not null)
                {
                    arts.Remove(art);
                }
            });

        return mockRepo;
    }
}
