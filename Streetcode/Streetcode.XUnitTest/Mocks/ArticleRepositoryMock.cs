// <copyright file="ArticleRepositoryMock.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.InfoBlocks.Articles;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    /// <summary>
    /// Mocks article repository.
    /// </summary>
    /// <returns>Returns mocked repository. </returns>
    public static Mock<IRepositoryWrapper> GetArticleRepositoryMock()
    {
        var mockRepo = new Mock<IRepositoryWrapper>();

        var articles = new List<Article>()
            {
                new Article { Id = 1, Title = "First Title", Text = "First Text"},
                new Article { Id = 2, Title = "Second Title", Text = "Second Text"},
                new Article { Id = 3, Title = "Third Title", Text = "Third Text"},
                new Article { Id = 4, Title = "Fourth Title", Text = "Fourth Text"},
            };

        mockRepo.Setup(x => x.ArticleRepository.GetAllAsync(It.IsAny<Expression<Func<Article, bool>>>(), It.IsAny<Func<IQueryable<Article>, IIncludableQueryable<Article, object>>>()))
            .ReturnsAsync(articles);

        mockRepo.Setup(x => x.ArticleRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Article, bool>>>(), It.IsAny<Func<IQueryable<Article>, IIncludableQueryable<Article, object>>>()))
            .ReturnsAsync((Expression<Func<Article, bool>> predicate, Func<IQueryable<Article>, IIncludableQueryable<Article, object>> include) =>
            {
                return articles.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.ArticleRepository.Create(It.IsAny<Article>())).Returns(articles[0]);

        mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        return mockRepo;
    }
}
