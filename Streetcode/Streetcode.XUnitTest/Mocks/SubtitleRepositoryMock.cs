namespace Streetcode.XUnitTest.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.AdditionalContent;
using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;
using Streetcode.DAL.Entities.News;
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

        mockRepo.Setup(x => x.SubtitleRepository.Create(It.IsAny<Subtitle>()))
            .Returns((Subtitle subtitle) =>
            {
                subtitles.Add(subtitle);
                return subtitle;
            });

        mockRepo.Setup(x => x.SubtitleRepository.Delete(It.IsAny<Subtitle>()))
            .Callback((Subtitle subtitle) =>
            {
                subtitle = subtitles.FirstOrDefault(x => x.Id == subtitle.Id);
                if (subtitle is not null)
                {
                    subtitles.Remove(subtitle);
                }
            });

        mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        return mockRepo;
    }
}
