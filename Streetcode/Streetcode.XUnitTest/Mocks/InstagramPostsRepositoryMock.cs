namespace Streetcode.XUnitTest.MediatRTests.Mocks;

using Moq;
using Streetcode.BLL.Interfaces.Instagram;
using Streetcode.DAL.Entities.Instagram;

internal partial class RepositoryMocker
{
    public static Mock<IInstagramService> GetInstagramPostsMock()
    {
        var posts = new List<InstagramPost>()
            {
                new InstagramPost { Id = "1", Caption = "1Caption", IsPinned = true, MediaType = "Image", MediaUrl = "1url", Permalink = "1permalink", ThumbnailUrl = "1thumbnailurl" },
                new InstagramPost { Id = "2", Caption = "2Caption", IsPinned = true, MediaType = "Image", MediaUrl = "2url", Permalink = "2permalink", ThumbnailUrl = "2thumbnailurl" },
                new InstagramPost { Id = "3", Caption = "3Caption", IsPinned = true, MediaType = "Image", MediaUrl = "3url", Permalink = "3permalink", ThumbnailUrl = "3thumbnailurl" },
                new InstagramPost { Id = "4", Caption = "4Caption", IsPinned = true, MediaType = "Image", MediaUrl = "4url", Permalink = "4permalink", ThumbnailUrl = "4thumbnailurl" },
            };

        var mockRepo = new Mock<IInstagramService>();

        mockRepo.Setup(x => x.GetPostsAsync())
            .ReturnsAsync(posts);

        return mockRepo;
    }
}
