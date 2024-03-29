﻿namespace Streetcode.XUnitTest.MediatRTests.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Streetcode.TextContent;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    public static Mock<IRepositoryWrapper> GetFactRepositoryMock()
    {
        var facts = new List<Fact>()
            {
                new Fact { Id = 1, Title = "1", StreetcodeId = 1},
                new Fact { Id = 2, Title = "2", StreetcodeId = 1},
                new Fact { Id = 3, Title = "3", StreetcodeId = 1},
                new Fact { Id = 4, Title = "4", StreetcodeId = 1},
                new Fact { Id = 5, Title = "5", StreetcodeId = 2},
                new Fact { Id = 6, Title = "6", StreetcodeId = 2},
                new Fact { Id = 7, Title = "7", StreetcodeId = 2},
                new Fact { Id = 8, Title = "8", StreetcodeId = 3},
                new Fact { Id = 9, Title = "9", StreetcodeId = 3},
            };

        var mockRepo = new Mock<IRepositoryWrapper>();

        mockRepo.Setup(x => x.FactRepository
        .GetAllAsync(
            It.IsAny<Expression<Func<Fact, bool>>>(),
            It.IsAny<Func<IQueryable<Fact>, IIncludableQueryable<Fact, object>>>()))
        .ReturnsAsync((
            Expression<Func<Fact, bool>> predicate,
            Func<IQueryable<Fact>, IIncludableQueryable<Fact, object>> include) =>
        {
            if (predicate is null)
            {
                return facts;
            }

            return facts.Where(predicate.Compile());
        });

        mockRepo.Setup(x => x.FactRepository
            .GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Fact, bool>>>(),
                It.IsAny<Func<IQueryable<Fact>, IIncludableQueryable<Fact, object>>>()))
            .ReturnsAsync((Expression<Func<Fact, bool>> predicate, Func<IQueryable<Fact>, IIncludableQueryable<Fact, object>> include) =>
            {
                return facts.FirstOrDefault(predicate.Compile());
            });

        return mockRepo;
    }
}
