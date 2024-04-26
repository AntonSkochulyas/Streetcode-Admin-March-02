namespace Streetcode.XUnitTest.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.AdditionalContent;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    public static Mock<IRepositoryWrapper> GetTagRepositoryMock()
    {
        List<Tag> tags = new List<Tag>
            {
                new Tag
                {
                    Id = 1,
                    Title = "Test",
                },
                new Tag
                {
                    Id = 2,
                    Title = "Test",
                },
            };

        var tagIndeces = new List<StreetcodeTagIndex>
            {
                new StreetcodeTagIndex { TagId = 1, StreetcodeId = 1, Tag = new Tag { Id = 1, Title = "Test" }, Index = 1 },
                new StreetcodeTagIndex { TagId = 2, StreetcodeId = 1, Tag = new Tag { Id = 2, Title = "Test" }, Index = 2 },
            };

        var mockRepo = new Mock<IRepositoryWrapper>();

        mockRepo.Setup(x => x.TagRepository.Create(It.IsAny<Tag>()))
            .Returns((Tag tag) =>
            {
                tags.Add(tag);
                return tag;
            });

        mockRepo.Setup(x => x.TagRepository.CreateAsync(It.IsAny<Tag>()))
            .ReturnsAsync((Tag tag) =>
            {
                tags.Add(tag);
                return tag;
            });

        mockRepo.Setup(x => x.TagRepository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<Tag, bool>>>(),
            It.IsAny<Func<IQueryable<Tag>, IIncludableQueryable<Tag, object>>>()))
            .ReturnsAsync((Expression<Func<Tag, bool>> predicate, Func<IQueryable<Tag>,
            IIncludableQueryable<Tag, object>> include) =>
            {
                return tags.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.TagRepository.GetAllAsync(
            It.IsAny<Expression<Func<Tag, bool>>>(),
            It.IsAny<Func<IQueryable<Tag>, IIncludableQueryable<Tag, object>>>()))
            .ReturnsAsync(tags);

        mockRepo.Setup(x => x.StreetcodeTagIndexRepository.GetAllAsync(
           It.IsAny<Expression<Func<StreetcodeTagIndex, bool>>>(),
           It.IsAny<Func<IQueryable<StreetcodeTagIndex>,
           IIncludableQueryable<StreetcodeTagIndex, object>>>()))
           .Returns(Task.FromResult<IEnumerable<StreetcodeTagIndex>>(tagIndeces));

        mockRepo.Setup(x => x.TagRepository.Delete(It.IsAny<Tag>()))
            .Callback((Tag tag) =>
            {
                tag = tags.FirstOrDefault(x => x.Id == tag.Id);
                if (tag is not null)
                {
                    tags.Remove(tag);
                }
            });

        mockRepo.Setup(repo => repo.TagRepository.Update(It.IsAny<Tag>()));

        mockRepo.Setup(x => x.SaveChanges()).Returns(1);

        return mockRepo;
    }

    public static Mock<IRepositoryWrapper> GetTagRepositoryMockWithSettingException()
    {
        var tag = new Tag()
        {
            Id = 1,
            Title = "Test",
        };

        List<Tag> tags = new List<Tag>
            {
                new Tag
                {
                    Id = 1,
                    Title = "Test",
                },
                new Tag
                {
                    Id = 2,
                    Title = "Test",
                },
            };

        var tagIndeces = new List<StreetcodeTagIndex>
            {
                new StreetcodeTagIndex { TagId = 1, StreetcodeId = 1, Tag = new Tag { Id = 1, Title = "Test" }, Index = 1 },
                new StreetcodeTagIndex { TagId = 2, StreetcodeId = 1, Tag = new Tag { Id = 2, Title = "Test" }, Index = 2 },
            };

        var mockRepo = new Mock<IRepositoryWrapper>();

        mockRepo.Setup(x => x.TagRepository.CreateAsync(It.IsAny<Tag>()))
            .ReturnsAsync((Tag tag) =>
            {
                tags.Add(tag);
                return tag;
            });

        mockRepo.Setup(x => x.TagRepository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<Tag, bool>>>(),
            It.IsAny<Func<IQueryable<Tag>, IIncludableQueryable<Tag, object>>>()))
            .ReturnsAsync((Expression<Func<Tag, bool>> predicate, Func<IQueryable<Tag>,
            IIncludableQueryable<Tag, object>> include) =>
            {
                return tags.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.TagRepository.GetAllAsync(
            It.IsAny<Expression<Func<Tag, bool>>>(),
            It.IsAny<Func<IQueryable<Tag>,
            IIncludableQueryable<Tag, object>>>()))
            .ReturnsAsync(tags);

        mockRepo.Setup(x => x.TagRepository.Update(It.IsAny<Tag>()));

        mockRepo.Setup(x => x.TagRepository.Delete(It.IsAny<Tag>()))
            .Callback((Tag tag) =>
            {
                tag = tags.FirstOrDefault(x => x.Id == tag.Id);
                if (tag is not null)
                {
                    tags.Remove(tag);
                }
            });

        mockRepo.Setup(x => x.SaveChanges()).Throws(new InvalidOperationException("Failed to create tag"));

        return mockRepo;
    }
}
