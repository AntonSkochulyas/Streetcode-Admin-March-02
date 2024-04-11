using System;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

namespace Streetcode.XUnitTest.Mocks
{
	internal partial class RepositoryMocker
	{
        /// <summary>
        /// Mocks image repository.
        /// </summary>
        /// <returns>Returns mocked repository. </returns>
        public static Mock<IRepositoryWrapper> GetStreetcodeImagesRepositoryMock()
        {
            var streetCodeImages = new List<StreetcodeImage>()
            {
            new StreetcodeImage
            {
                ImageId = 1,
                Image = new Image(),
                StreetcodeId = 1,
                Streetcode = new DAL.Entities.Streetcode.StreetcodeContent()
            },
            new StreetcodeImage
            {
                ImageId = 2,
                Image = new Image(),
                StreetcodeId = 2,
                Streetcode = new DAL.Entities.Streetcode.StreetcodeContent()
            },
            new StreetcodeImage
            {
                ImageId = 3,
                Image = new Image(),
                StreetcodeId = 3,
                Streetcode = new DAL.Entities.Streetcode.StreetcodeContent()
            },
            new StreetcodeImage
            {
                ImageId = 4,
                Image = new Image(),
                StreetcodeId = 4,
                Streetcode = new DAL.Entities.Streetcode.StreetcodeContent()
            },
            };

            var mockRepo = new Mock<IRepositoryWrapper>();

            mockRepo.Setup(x => x.StreetcodeImageRepository.GetAllAsync(It.IsAny<Expression<Func<StreetcodeImage, bool>>>(), It.IsAny<Func<IQueryable<StreetcodeImage>, IIncludableQueryable<StreetcodeImage, object>>>()))
                .ReturnsAsync(streetCodeImages);

            mockRepo.Setup(x => x.StreetcodeImageRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<StreetcodeImage, bool>>>(), It.IsAny<Func<IQueryable<StreetcodeImage>, IIncludableQueryable<StreetcodeImage, object>>>()))
                .ReturnsAsync((Expression<Func<StreetcodeImage, bool>> predicate, Func<IQueryable<StreetcodeImage>, IIncludableQueryable<StreetcodeImage, object>> include) =>
                {
                    return streetCodeImages.FirstOrDefault(predicate.Compile());
                });

            mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            return mockRepo;
        }
    }
}