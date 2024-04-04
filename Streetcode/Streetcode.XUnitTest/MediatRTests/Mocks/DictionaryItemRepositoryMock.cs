// <copyright file="DictionaryItemRepositoryMock.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Dictionaries;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    /// <summary>
    /// Mocks dictionary item repository.
    /// </summary>
    /// <returns>Returns mocked repository. </returns>
    public static Mock<IRepositoryWrapper> GetDictionaryItemRepositoryMock()
    {
        var mockRepo = new Mock<IRepositoryWrapper>();

        var dictionaryItems = new List<DictionaryItem>()
            {
                new DictionaryItem { Id = 1, Name = "First Dictionary", Description = "First Description"},
                new DictionaryItem { Id = 2, Name = "Second Dictionary", Description = "Second Description"},
                new DictionaryItem { Id = 3, Name = "Third Dictionary", Description = "Third Description"},
                new DictionaryItem { Id = 4, Name = "Fourth Dictionary", Description = "Fourth Description"},
            };

        mockRepo.Setup(x => x.DictionaryItemRepository.GetAllAsync(It.IsAny<Expression<Func<DictionaryItem, bool>>>(), It.IsAny<Func<IQueryable<DictionaryItem>, IIncludableQueryable<DictionaryItem, object>>>()))
            .ReturnsAsync(dictionaryItems);

        mockRepo.Setup(x => x.DictionaryItemRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<DictionaryItem, bool>>>(), It.IsAny<Func<IQueryable<DictionaryItem>, IIncludableQueryable<DictionaryItem, object>>>()))
            .ReturnsAsync((Expression<Func<DictionaryItem, bool>> predicate, Func<IQueryable<DictionaryItem>, IIncludableQueryable<DictionaryItem, object>> include) =>
            {
                return dictionaryItems.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.DictionaryItemRepository.Create(It.IsAny<DictionaryItem>())).Returns(dictionaryItems[0]);

        mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        return mockRepo;
    }
}
