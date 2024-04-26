namespace Streetcode.XUnitTest.Mocks;

using Ardalis.Specification;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.BLL.MediatR.Sources.SourceLink.GetCategoriesByStreetcodeId;
using Streetcode.DAL.Entities.AdditionalContent;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Entities.News;
using Streetcode.DAL.Entities.Sources;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Repositories.Realizations.Source;
using Streetcode.DAL.Specification.Sources.SourceLinkCategory;
using Streetcode.DAL.Specification.Sources.StreetcodeCategoryContent;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    public static Mock<IRepositoryWrapper> GetSourceRepositoryMock()
    {
        var images = new List<Image>()
        {
            new Image() { Id = 1, Base64 = "TestBlob1" },
            new Image() { Id = 2, Base64 = "TestBlob2" },
            new Image() { Id = 3, Base64 = "TestBlob3" },
            new Image() { Id = 4, Base64 = "TestBlob4" }
        };

        var streetcodeContents = new List<StreetcodeContent>()
        {
           new StreetcodeContent()
           {
               Id = 1,
           },
           new StreetcodeContent()
           {
               Id = 2,
           },
        };

        var sources = new List<SourceLinkCategory>()
        {
            new SourceLinkCategory { Id = 1, Title = "First title", ImageId = 1, Image = images[0], Streetcodes = streetcodeContents },
            new SourceLinkCategory { Id = 2, Title = "Second title", ImageId = 2, Image = images[1], Streetcodes = streetcodeContents },
            new SourceLinkCategory { Id = 3, Title = "Third title", ImageId = 3, Image = images[2], Streetcodes = streetcodeContents },
            new SourceLinkCategory { Id = 4, Title = "Fourth title", ImageId = 4, Image = images[3] },
        };

        var streetcodeCategoryContents = new List<StreetcodeCategoryContent>()
        {
            new StreetcodeCategoryContent() { SourceLinkCategoryId = 1, StreetcodeId = 1, Text = "Test1" },
            new StreetcodeCategoryContent() { SourceLinkCategoryId = 2, StreetcodeId = 1, Text = "Test2" },
        };

        var mockRepo = new Mock<IRepositoryWrapper>();

        mockRepo.Setup(x => x.SourceCategoryRepository.GetAllAsync(It.IsAny<Expression<Func<SourceLinkCategory, bool>>>(), It.IsAny<Func<IQueryable<SourceLinkCategory>, IIncludableQueryable<SourceLinkCategory, object>>>()))
            .ReturnsAsync(sources);

        mockRepo.Setup(x => x.StreetcodeCategoryContentRepository.GetAllAsync(
            It.IsAny<Expression<Func<StreetcodeCategoryContent, bool>>>(),
            It.IsAny<Func<IQueryable<StreetcodeCategoryContent>,
            IIncludableQueryable<StreetcodeCategoryContent, object>>>()))
            .ReturnsAsync(streetcodeCategoryContents);

        mockRepo.Setup(repo => repo.SourceCategoryRepository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<SourceLinkCategory, bool>>>(),
            It.IsAny<Func<IQueryable<SourceLinkCategory>,
            IIncludableQueryable<SourceLinkCategory, object>>>()))
            .ReturnsAsync(
            (
                Expression<Func<SourceLinkCategory, bool>> predicate,
                Func<IQueryable<SourceLinkCategory>,
                IIncludableQueryable<SourceLinkCategory, object>> include) =>
            {
                return sources.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(repo => repo.SourceCategoryRepository.GetItemBySpecAsync(
        It.IsAny<ISpecification<SourceLinkCategory>>()))
        .ReturnsAsync((GetByIdSourceLinkCategorySpec spec) =>
        {
            int id = spec.Id;

            var category = sources.FirstOrDefault(s => s.Id == id);

            return category;
        });

        mockRepo.Setup(repo => repo.SourceCategoryRepository.GetItemsBySpecAsync(
        It.IsAny<ISpecification<SourceLinkCategory>>()))
        .ReturnsAsync((GetByStreetcodeIdSourceLinkCategorySpec spec) =>
        {
            int streetcodeId = spec.StreetcodeId;

            var category = sources.Where(s => s.Streetcodes.Any(s => s.Id == streetcodeId));

            return category;
        });

        mockRepo.Setup(repo => repo.StreetcodeCategoryContentRepository.GetItemBySpecAsync(
        It.IsAny<ISpecification<StreetcodeCategoryContent>>()))
        .ReturnsAsync((GetByStreetcodeIdStreetcodeCategoryContentSpec spec) =>
        {
            int sourceLinkId = spec.SourceLinkId;
            int streetcodeId = spec.StreetcodeId;

            var categoryContent = streetcodeCategoryContents.FirstOrDefault(s => s.SourceLinkCategoryId == sourceLinkId && s.StreetcodeId == streetcodeId);

            return categoryContent;
        });

        mockRepo.Setup(repo => repo.StreetcodeCategoryContentRepository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<StreetcodeCategoryContent, bool>>>(),
            It.IsAny<Func<IQueryable<StreetcodeCategoryContent>,
            IIncludableQueryable<StreetcodeCategoryContent, object>>>()))
            .ReturnsAsync(
            (
                Expression<Func<StreetcodeCategoryContent, bool>> predicate,
                Func<IQueryable<StreetcodeCategoryContent>,
                IIncludableQueryable<StreetcodeCategoryContent, object>> include) =>
            {
                return streetcodeCategoryContents.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(repo => repo.StreetcodeRepository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
            It.IsAny<Func<IQueryable<StreetcodeContent>,
            IIncludableQueryable<StreetcodeContent, object>>>()))
            .ReturnsAsync(
            (
                Expression<Func<StreetcodeContent, bool>> predicate,
                Func<IQueryable<StreetcodeContent>,
                IIncludableQueryable<StreetcodeContent, object>> include) =>
            {
                return streetcodeContents.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(repo => repo.ImageRepository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<Image, bool>>>(),
            It.IsAny<Func<IQueryable<Image>,
            IIncludableQueryable<Image, object>>>()))
            .ReturnsAsync(
            (
                Expression<Func<Image, bool>> predicate,
                Func<IQueryable<Image>,
                IIncludableQueryable<Image, object>> include) =>
            {
                return images.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.SourceCategoryRepository
                .GetAllAsync(
                    It.IsAny<Expression<Func<SourceLinkCategory, bool>>>(),
                    It.IsAny<Func<IQueryable<SourceLinkCategory>, IIncludableQueryable<SourceLinkCategory, object>>>()))
                .ReturnsAsync(sources);

        mockRepo.Setup(x => x.StreetcodeCategoryContentRepository.Create(It.IsAny<StreetcodeCategoryContent>()))
            .Returns((StreetcodeCategoryContent streetcodeContent) =>
            {
                streetcodeCategoryContents.Add(streetcodeContent);
                return streetcodeContent;
            });

        mockRepo.Setup(x => x.SourceCategoryRepository.Create(It.IsAny<SourceLinkCategory>()))
            .Returns((SourceLinkCategory sourceLinkCategory) =>
            {
                sources.Add(sourceLinkCategory);
                return sourceLinkCategory;
            });

        mockRepo.Setup(x => x.SourceCategoryRepository.Delete(It.IsAny<SourceLinkCategory>()))
            .Callback((SourceLinkCategory sourceLinkCategory) =>
            {
                sourceLinkCategory = sources.FirstOrDefault(x => x.Id == sourceLinkCategory.Id);
                if (sourceLinkCategory is not null)
                {
                    sources.Remove(sourceLinkCategory);
                }
            });

        mockRepo.Setup(x => x.StreetcodeCategoryContentRepository.Delete(It.IsAny<StreetcodeCategoryContent>()))
            .Callback((StreetcodeCategoryContent streetcodeCategoryContent) =>
            {
                streetcodeCategoryContent = streetcodeCategoryContents.FirstOrDefault(
                    x => x.SourceLinkCategoryId == streetcodeCategoryContent.SourceLinkCategoryId &&
                    x.StreetcodeId == streetcodeCategoryContent.StreetcodeId);

                if (streetcodeCategoryContent is not null)
                {
                    streetcodeCategoryContents.Remove(streetcodeCategoryContent);
                }
            });

        mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        return mockRepo;
    }
}
