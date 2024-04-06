namespace Streetcode.XUnitTest.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Entities.Timeline;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    /// <summary>
    /// Mocks streetcode repository.
    /// </summary>
    /// <returns>Returns mocked repository. </returns>
    public static Mock<IRepositoryWrapper> GetStreetcodeRepositoryMock()
    {
        var streetCodes = new List<StreetcodeContent>
            {
                new StreetcodeContent() { Id = 1, Title = "First streetcode content title", TransliterationUrl = "first-streetcode", DateString = "2024-04-05" },
                new StreetcodeContent() { Id = 2, Title = "Second streetcode content title", TransliterationUrl = "second-streetcode", DateString = "2024-04-06" },
                new StreetcodeContent() { Id = 3, Title = "Third streetcode content title", TransliterationUrl = "third-streetcode", DateString = "2024-04-07" },
                new StreetcodeContent() { Id = 4, Title = "Fourth streetcode content title", TransliterationUrl = "fourth-streetcode", DateString = "2024-04-08" },
            };

        var mockRepo = new Mock<IRepositoryWrapper>();

        mockRepo.Setup(x => x.StreetcodeRepository.GetAllAsync(It.IsAny<Expression<Func<StreetcodeContent, bool>>>(), It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
            .ReturnsAsync(streetCodes);

        mockRepo.Setup(x => x.StreetcodeRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<StreetcodeContent, bool>>>(), It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
            .ReturnsAsync((Expression<Func<StreetcodeContent, bool>> predicate, Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>> include) =>
            {
                return streetCodes.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.StreetcodeRepository.Create(It.IsAny<StreetcodeContent>()))
            .Returns((StreetcodeContent streetcode) =>
            {
                streetCodes.Add(streetcode);
                return streetcode;
            });

        mockRepo.Setup(x => x.StreetcodeRepository.Delete(It.IsAny<StreetcodeContent>()))
            .Callback((StreetcodeContent streetcode) =>
            {
                streetcode = streetCodes.FirstOrDefault(x => x.Id == streetcode.Id);
                if (streetcode != null)
                {
                    streetCodes.Remove(streetcode);
                }
            });

        return mockRepo;
    }
}
