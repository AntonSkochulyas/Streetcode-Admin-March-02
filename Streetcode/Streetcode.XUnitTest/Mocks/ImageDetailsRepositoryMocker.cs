using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.XUnitTest.Mocks
{
	internal partial class RepositoryMocker
	{
        /// <summary>
        /// Mocks image repository.
        /// </summary>
        /// <returns>Returns mocked repository. </returns>
        public static Mock<IRepositoryWrapper> GetImageDetailsRepositoryMock()
        {
            var imageDetails = new List<ImageDetails>()
            {
            new ImageDetails
            {
                Id = 1,
                Alt = "1Alt",
                Title = "1Title",
                Image = new Image(),
                ImageId = 1
            },
            new ImageDetails
            {
                Id = 2,
                Alt = "2Alt",
                Title = "2Title",
                Image = new Image(),
                ImageId = 2
            },
            new ImageDetails
            {
                Id = 3,
                Alt = "3Alt",
                Title = "3Title",
                Image = new Image(),
                ImageId = 3
            },
            new ImageDetails
            {
                Id = 4,
                Alt = "4Alt",
                Title = "4Title",
                Image = new Image(),
                ImageId = 4
            },
            };

            var mockRepo = new Mock<IRepositoryWrapper>();

            mockRepo.Setup(x => x.ImageDetailsRepository.GetAllAsync(It.IsAny<Expression<Func<ImageDetails, bool>>>(), It.IsAny<Func<IQueryable<ImageDetails>, IIncludableQueryable<ImageDetails, object>>>()))
                .ReturnsAsync(imageDetails);

            mockRepo.Setup(x => x.ImageDetailsRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<ImageDetails, bool>>>(), It.IsAny<Func<IQueryable<ImageDetails>, IIncludableQueryable<ImageDetails, object>>>()))
                .ReturnsAsync((Expression<Func<ImageDetails, bool>> predicate, Func<IQueryable<ImageDetails>, IIncludableQueryable<ImageDetails, object>> include) =>
                {
                    return imageDetails.FirstOrDefault(predicate.Compile());
                });

            mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            return mockRepo;
        }
    }
}