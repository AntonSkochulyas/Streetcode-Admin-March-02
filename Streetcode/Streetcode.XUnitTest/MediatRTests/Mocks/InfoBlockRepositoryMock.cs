// <copyright file="InfoBlockRepositoryMock.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.InfoBlocks.Articles;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes;
using Streetcode.DAL.Entities.InfoBlocks;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    /// <summary>
    /// Mocks info block repository.
    /// </summary>
    /// <returns>Returns mocked repository. </returns>
    public static Mock<IRepositoryWrapper> GetInfoBlockRepositoryMock()
    {
        var mockRepo = new Mock<IRepositoryWrapper>();

        var infoBlocks = new List<InfoBlock>()
            {
                new InfoBlock
                {
                    Id = 1,
                    ArticleId = 1,
                    Article = new Article { Id = 1, Text = "First Text", Title = "First Title" },
                    VideoURL = "www.youtube.com",
                    AuthorShipId = 1,
                    AuthorShip = new AuthorShip
                    {
                        Id = 1,
                        Text = "First Text",
                        AuthorShipHyperLinkId = 1,
                        AuthorShipHyperLink = new AuthorShipHyperLink { Id = 1, Title = "First Title", URL = "First URL"},
                    },
                },
                new InfoBlock
                {
                    Id = 2,
                    ArticleId = 2,
                    Article = new Article { Id = 2, Text = "Second Text", Title = "Second Title" },
                    VideoURL = "www.youtube2.com",
                    AuthorShipId = 2,
                    AuthorShip = new AuthorShip
                    {
                        Id = 2,
                        Text = "Second Text",
                        AuthorShipHyperLinkId = 2,
                        AuthorShipHyperLink = new AuthorShipHyperLink { Id = 2, Title = "Second Title", URL = "Second URL"},
                    },
                },
                new InfoBlock
                {
                    Id = 3,
                    ArticleId = 3,
                    Article = new Article { Id = 3, Text = "Third Text", Title = "Third Title" },
                    VideoURL = "www.youtube3.com",
                    AuthorShipId = 3,
                    AuthorShip = new AuthorShip
                    {
                        Id = 3,
                        Text = "Third Text",
                        AuthorShipHyperLinkId = 3,
                        AuthorShipHyperLink = new AuthorShipHyperLink { Id = 3, Title = "Third Title", URL = "Third URL"},
                    },
                },
                new InfoBlock
                {
                    Id = 4,
                    ArticleId = 4,
                    Article = new Article { Id = 4, Text = "Fourth Text", Title = "Fourth Title" },
                    VideoURL = "www.youtube4.com",
                    AuthorShipId = 4,
                    AuthorShip = new AuthorShip
                    {
                        Id = 4,
                        Text = "Fourth Text",
                        AuthorShipHyperLinkId = 4,
                        AuthorShipHyperLink = new AuthorShipHyperLink { Id = 4, Title = "Fourth Title", URL = "Fourth URL"},
                    },
                },
            };

        mockRepo.Setup(x => x.InfoBlockRepository.GetAllAsync(It.IsAny<Expression<Func<InfoBlock, bool>>>(), It.IsAny<Func<IQueryable<InfoBlock>, IIncludableQueryable<InfoBlock, object>>>()))
            .ReturnsAsync(infoBlocks);

        mockRepo.Setup(x => x.InfoBlockRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<InfoBlock, bool>>>(), It.IsAny<Func<IQueryable<InfoBlock>, IIncludableQueryable<InfoBlock, object>>>()))
            .ReturnsAsync((Expression<Func<InfoBlock, bool>> predicate, Func<IQueryable<InfoBlock>, IIncludableQueryable<InfoBlock, object>> include) =>
            {
                return infoBlocks.FirstOrDefault(predicate.Compile());
            });

        return mockRepo;
    }
}
