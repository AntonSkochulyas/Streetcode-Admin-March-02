namespace Streetcode.XUnitTest.MediatRTests.Mocks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Entities.News;
using Streetcode.DAL.Repositories.Interfaces.Base;

internal partial class RepositoryMocker
{
    public static Mock<IRepositoryWrapper> GetNewsRepositoryMock()
    {
        var newsItem = new News()
        {
            Id = 1,
            Title = "Title1",
            Text = "Text1",
            CreationDate = new DateTime(2024, 3, 21, 0, 0, 0, DateTimeKind.Utc),
            ImageId = 1,
            URL = "example.com",
        };

        var news = new List<News>()
            {
                new News()
                {
                Id = 1,
                Title = "Title1",
                Text = "Text1",
                CreationDate = new DateTime(2024, 3, 21, 0, 0, 0, DateTimeKind.Utc),
                ImageId = 1,
                URL = "example.com",
                },
                new News()
                {
                    Id = 1,
                    Title = "Title1",
                    Text = "Text1",
                    CreationDate = new DateTime(2024, 3, 22, 0, 0, 0, DateTimeKind.Utc),
                    ImageId = 1,
                    URL = "example1.com",
                },
                new News()
                {
                    Id = 2,
                    Title = "Title2",
                    Text = "Text2",
                    CreationDate = new DateTime(2022, 3, 21, 0, 0, 0, DateTimeKind.Utc),
                    ImageId = 2,
                    URL = "example2.com",
                },
                new News()
                {
                    Id = 3,
                    Title = "Title3",
                    Text = "Text3",
                    CreationDate = new DateTime(2024, 3, 23, 0, 0, 0, DateTimeKind.Utc),
                    ImageId = 3,
                    URL = "example3.com",
                },
            };

        var images = new List<Image>()
            {
                new Image()
                {
                    Id = 1
                },
                new Image()
                {
                    Id = 2
                },
                new Image()
                {
                    Id = 3
                },
            };

        var mockRepo = new Mock<IRepositoryWrapper>();

        mockRepo.Setup(x => x.NewsRepository.Create(It.IsAny<News>()))
            .Returns(newsItem);

        mockRepo.Setup(x => x.NewsRepository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<News, bool>>>(),
            It.IsAny<Func<IQueryable<News>, IIncludableQueryable<News, object>>>()))
            .ReturnsAsync((Expression<Func<News, bool>> predicate, Func<IQueryable<News>,
            IIncludableQueryable<News, object>> include) =>
            {
                return news.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.NewsRepository.GetAllAsync(
            It.IsAny<Expression<Func<News, bool>>>(),
            It.IsAny<Func<IQueryable<News>, IIncludableQueryable<News, object>>>()))
            .ReturnsAsync(news);

        mockRepo.Setup(x => x.ImageRepository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<Image, bool>>>(),
            It.IsAny<Func<IQueryable<Image>, IIncludableQueryable<Image, object>>>()))
            .ReturnsAsync((Expression<Func<Image, bool>> predicate, Func<IQueryable<Image>,
            IIncludableQueryable<Image, object>> include) =>
            {
                return images.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        return mockRepo;
    }
}
