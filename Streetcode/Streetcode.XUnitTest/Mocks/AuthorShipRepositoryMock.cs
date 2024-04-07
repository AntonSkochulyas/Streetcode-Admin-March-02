// <copyright file="AuthorShipRepositoryMock.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    /// <summary>
    /// Mocks authorship repository.
    /// </summary>
    /// <returns>Returns mocked repository. </returns>
    public static Mock<IRepositoryWrapper> GetAuthorShipRepositoryMock()
    {
        var mockRepo = new Mock<IRepositoryWrapper>();

        var authorShips = new List<AuthorShip>()
            {
                new AuthorShip
                {
                    Id = 1,
                    Text = "First Text",
                    AuthorShipHyperLinkId = 1,
                    AuthorShipHyperLink = new AuthorShipHyperLink { Id = 1, Title = "First Title", URL = "First URL"},
                },
                new AuthorShip
                {
                    Id = 2,
                    Text = "Second Text",
                    AuthorShipHyperLinkId = 2,
                    AuthorShipHyperLink = new AuthorShipHyperLink { Id = 2, Title = "Second Title", URL = "Second URL"},
                },
                new AuthorShip
                {
                    Id = 3,
                    Text = "Third Text",
                    AuthorShipHyperLinkId = 3,
                    AuthorShipHyperLink = new AuthorShipHyperLink { Id = 3, Title = "Third Title", URL = "Third URL"},
                },
                new AuthorShip
                {
                    Id = 4,
                    Text = "Fourth Text",
                    AuthorShipHyperLinkId = 4,
                    AuthorShipHyperLink = new AuthorShipHyperLink { Id = 4, Title = "Fourth Title", URL = "Fourth URL"},
                },
            };

        mockRepo.Setup(x => x.AuthorShipRepository.GetAllAsync(It.IsAny<Expression<Func<AuthorShip, bool>>>(), It.IsAny<Func<IQueryable<AuthorShip>, IIncludableQueryable<AuthorShip, object>>>()))
            .ReturnsAsync(authorShips);

        mockRepo.Setup(x => x.AuthorShipRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<AuthorShip, bool>>>(), It.IsAny<Func<IQueryable<AuthorShip>, IIncludableQueryable<AuthorShip, object>>>()))
            .ReturnsAsync((Expression<Func<AuthorShip, bool>> predicate, Func<IQueryable<AuthorShip>, IIncludableQueryable<AuthorShip, object>> include) =>
            {
                return authorShips.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.AuthorShipRepository.Create(It.IsAny<AuthorShip>())).Returns(authorShips[0]);

        mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        return mockRepo;
    }
}