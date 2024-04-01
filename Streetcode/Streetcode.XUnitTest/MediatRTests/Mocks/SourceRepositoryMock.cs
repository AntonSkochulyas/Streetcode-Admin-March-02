﻿namespace Streetcode.XUnitTest.MediatRTests.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Entities.Sources;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    public static Mock<IRepositoryWrapper> GetSourceRepositoryMock()
    {
        var sources = new List<SourceLinkCategory>()
        {
            new SourceLinkCategory { Id = 1, Title = "First title", ImageId = 1, Image = new Image() { Id = 1, Base64 = "TestBlob1", } },
            new SourceLinkCategory { Id = 2, Title = "Second title", ImageId = 2, Image = new Image() { Id = 2, Base64 = "TestBlob2", } },
            new SourceLinkCategory { Id = 3, Title = "Third title", ImageId = 3, Image = new Image() { Id = 3, Base64 = "TestBlob3", } },
            new SourceLinkCategory { Id = 4, Title = "Fourth title", ImageId = 4, Image = new Image() { Id = 4, Base64 = "TestBlob4", } },
        };

        var streetcodeCategoryContents = new List<StreetcodeCategoryContent>()
        {
            new StreetcodeCategoryContent() { SourceLinkCategoryId = 1, StreetcodeId = 1, Text = "Test1" },
            new StreetcodeCategoryContent() { SourceLinkCategoryId = 2, StreetcodeId = 1, Text = "Test2" },
        };

        var streetcodeContents = new List<StreetcodeContent>()
        {
           new StreetcodeContent()
           {
               Id = 1,
           },
           new StreetcodeContent()
           {
               Id = 1,
           },
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

        mockRepo.Setup(x => x.SourceCategoryRepository.Create(It.IsAny<SourceLinkCategory>())).Returns(sources[0]);
        mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        return mockRepo;
    }
}
