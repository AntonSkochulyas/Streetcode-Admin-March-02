namespace Streetcode.XUnitTest.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using Moq;
using Streetcode.DAL.Entities.Media;
using Streetcode.DAL.Entities.Sources;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    /// <summary>
    /// Mocks audio repository.
    /// </summary>
    /// <returns>Returns mocked repository. </returns>
    public static Mock<IRepositoryWrapper> GetAudiosRepositoryMock()
    {
        var audios = new List<Audio>()
            {
               new Audio { Id = 1, Title = "First audio", BlobName = "First blob name", MimeType = "First mime type", Base64 = "First base 64", Streetcode = null, },
               new Audio { Id = 2, Title = "Second audio", BlobName = "Second blob name", MimeType = "Second mime type", Base64 = "Second base 64", Streetcode = null },
               new Audio { Id = 3, Title = "Third audio", BlobName = "Third blob name", MimeType = "Third mime type", Base64 = "Third base 64", Streetcode = null },
               new Audio { Id = 4, Title = "Fourth audio", BlobName = "Fourth blob name", MimeType = "Fourth mime type", Base64 = "Fourth base 64", Streetcode = null },
            };

        var streetCodes = new List<StreetcodeContent>
            {
                new StreetcodeContent() { Id = 1, Title = "First streetcode content title", AudioId = 1, Audio = audios[0] },
                new StreetcodeContent() { Id = 2, Title = "Second streetcode content title", AudioId = 2 },
                new StreetcodeContent() { Id = 3, Title = "Third streetcode content title", AudioId = 3 },
                new StreetcodeContent() { Id = 4, Title = "Fourth streetcode content title", AudioId = 4 },
            };

        var mockRepo = new Mock<IRepositoryWrapper>();

        mockRepo.Setup(x => x.AudioRepository.GetAllAsync(It.IsAny<Expression<Func<Audio, bool>>>(), It.IsAny<Func<IQueryable<Audio>, IIncludableQueryable<Audio, object>>>()))
            .ReturnsAsync(audios);

        mockRepo.Setup(x => x.AudioRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Audio, bool>>>(), It.IsAny<Func<IQueryable<Audio>, IIncludableQueryable<Audio, object>>>()))
            .ReturnsAsync((Expression<Func<Audio, bool>> predicate, Func<IQueryable<Audio>, IIncludableQueryable<Audio, object>> include) =>
            {
                return audios.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.StreetcodeRepository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
            It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
            .ReturnsAsync((Expression<Func<StreetcodeContent, bool>> predicate, Func<IQueryable<StreetcodeContent>,
            IIncludableQueryable<StreetcodeContent, object>> include) =>
            {
                return streetCodes.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.AudioRepository.Create(It.IsAny<Audio>()))
            .Returns((Audio audio) =>
            {
                audios.Add(audio);
                return audio;
            });

        mockRepo.Setup(x => x.AudioRepository.Delete(It.IsAny<Audio>()))
            .Callback((Audio audio) =>
            {
                audio = audios.FirstOrDefault(x => x.Id == audio.Id);
                if (audio is not null)
                {
                    audios.Remove(audio);
                }
            });

        mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        return mockRepo;
    }
}
