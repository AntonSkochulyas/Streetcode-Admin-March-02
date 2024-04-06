namespace Streetcode.XUnitTest.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    /// <summary>
    /// Mocks image repository.
    /// </summary>
    /// <returns>Returns mocked repository. </returns>
    public static Mock<IRepositoryWrapper> GetImagesRepositoryMock()
    {
        var images = new List<Image>()
            {
            new Image
            {
                Id = 1,
                Base64 = "First base 64",
                BlobName = "First image blob name",
                MimeType = "First image mime type",
            },
            new Image
            {
                Id = 2,
                Base64 = "Second base 64",
                BlobName = "Second image blob name",
                MimeType = "Second image mime type",
            },
            new Image
            {
                Id = 3,
                Base64 = "Third base 64",
                BlobName = "Third image blob name",
                MimeType = "Third image mime type",
            },
            new Image
            {
                Id = 4,
                Base64 = "Fourth base 64",
                BlobName = "Fourth image blob name",
                MimeType = "Fourth image mime type",
            },
            };

        var mockRepo = new Mock<IRepositoryWrapper>();

        mockRepo.Setup(x => x.ImageRepository.GetAllAsync(It.IsAny<Expression<Func<Image, bool>>>(), It.IsAny<Func<IQueryable<Image>, IIncludableQueryable<Image, object>>>()))
            .ReturnsAsync(images);

        mockRepo.Setup(x => x.ImageRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Image, bool>>>(), It.IsAny<Func<IQueryable<Image>, IIncludableQueryable<Image, object>>>()))
            .ReturnsAsync((Expression<Func<Image, bool>> predicate, Func<IQueryable<Image>, IIncludableQueryable<Image, object>> include) =>
            {
                return images.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        return mockRepo;
    }
}
