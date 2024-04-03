// <copyright file="AuthorShipHyperLinkRepositoryMock.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    /// <summary>
    /// Mocks authorship hyper link repository.
    /// </summary>
    /// <returns>Returns mocked repository. </returns>
    public static Mock<IRepositoryWrapper> GetAuthorShipHyperLinkRepositoryMock()
    {
        var mockRepo = new Mock<IRepositoryWrapper>();

        var authorShipHyperLinks = new List<AuthorShipHyperLink>()
            {
                new AuthorShipHyperLink { Id = 1, Title = "First Title", URL = "First URL"},
                new AuthorShipHyperLink { Id = 2, Title = "Second Title", URL = "Second URL"},
                new AuthorShipHyperLink { Id = 3, Title = "Third Title", URL = "Third URL"},
                new AuthorShipHyperLink { Id = 4, Title = "Fourth Title", URL = "Fourth URL"},
            };

        mockRepo.Setup(x => x.AuthorShipHyperLinkRepository.GetAllAsync(It.IsAny<Expression<Func<AuthorShipHyperLink, bool>>>(), It.IsAny<Func<IQueryable<AuthorShipHyperLink>, IIncludableQueryable<AuthorShipHyperLink, object>>>()))
            .ReturnsAsync(authorShipHyperLinks);

        mockRepo.Setup(x => x.AuthorShipHyperLinkRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<AuthorShipHyperLink, bool>>>(), It.IsAny<Func<IQueryable<AuthorShipHyperLink>, IIncludableQueryable<AuthorShipHyperLink, object>>>()))
            .ReturnsAsync((Expression<Func<AuthorShipHyperLink, bool>> predicate, Func<IQueryable<AuthorShipHyperLink>, IIncludableQueryable<AuthorShipHyperLink, object>> include) =>
            {
                return authorShipHyperLinks.FirstOrDefault(predicate.Compile());
            });

        return mockRepo;
    }
}
