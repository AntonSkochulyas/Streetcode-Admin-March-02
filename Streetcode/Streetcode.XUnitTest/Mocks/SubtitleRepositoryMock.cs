namespace Streetcode.XUnitTest.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.AdditionalContent;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    public static Mock<IRepositoryWrapper> GetSubtitleRepositoryMock()
    {
        var mockRepo = new Mock<IRepositoryWrapper>();

        var subtitles = new List<Subtitle>()
            {
                new Subtitle
                {
                    StreetcodeId = 1,
                    Id = 1,
                    SubtitleText = "Test",
                },
                new Subtitle
                {
                    StreetcodeId = 1,
                    Id = 2,
                    SubtitleText = "Test",
                },
                new Subtitle
                {
                    StreetcodeId = 1,
                    Id = 3,
                    SubtitleText = "Test",
                },
            };

        mockRepo.Setup(x => x.SubtitleRepository.GetAllAsync(
            It.IsAny<Expression<Func<Subtitle, bool>>>(),
            It.IsAny<Func<IQueryable<Subtitle>, IIncludableQueryable<Subtitle, object>>>()))
            .ReturnsAsync(subtitles);

        mockRepo.Setup(x => x.SubtitleRepository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<Subtitle, bool>>>(),
            It.IsAny<Func<IQueryable<Subtitle>, IIncludableQueryable<Subtitle, object>>>()))
        .ReturnsAsync((
            Expression<Func<Subtitle, bool>> predicate,
            Func<IQueryable<Subtitle>,
            IIncludableQueryable<Subtitle, object>> include) =>
        {
            return subtitles.FirstOrDefault(predicate.Compile());
        });

        return mockRepo;
    }
}
