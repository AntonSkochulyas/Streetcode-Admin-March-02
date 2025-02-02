﻿namespace Streetcode.XUnitTest.Mocks;

using Ardalis.Specification;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using Moq;
using Streetcode.BLL.MediatR.Toponyms.GetAll;
using Streetcode.DAL.Entities.Sources;
using Streetcode.DAL.Entities.Toponyms;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Sources.SourceLinkCategory;
using Streetcode.DAL.Specification.Sources.StreetcodeCategoryContent;
using Streetcode.DAL.Specification.Toponyms;
using System.Linq.Expressions;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

internal partial class RepositoryMocker
{
    /// <summary>
    /// Mocks toponyms repository.
    /// </summary>
    /// <returns>Returns mocked repository. </returns>
    public static Mock<IRepositoryWrapper> GetToponymsRepositoryMock()
    {
        var toponyms = new List<Toponym>()
            {
                new Toponym()
                {
                    Id = 1,
                    Community = "First community",
                    AdminRegionNew = "First region new",
                    AdminRegionOld = "First region old",
                    Oblast = "First",
                    StreetName = "First streetname",
                },
                new Toponym()
                {
                    Id = 2,
                    Community = "Second community",
                    AdminRegionNew = "Second region new",
                    AdminRegionOld = "Second region old",
                    Oblast = "Second",
                    StreetName = "Second streetname"
                },
                new Toponym()
                {
                    Id = 3,
                    Community = "Third community",
                    AdminRegionNew = "Third region new",
                    AdminRegionOld = "Third region old",
                    Oblast = "Third",
                    StreetName = "Third streetname"
                },
                new Toponym()
                {
                    Id = 4,
                    Community = "Fourth community",
                    AdminRegionNew = "Fourth region new",
                    AdminRegionOld = "Fourth region old",
                    Oblast = "Fourth",
                    StreetName = "Fourth streetname"
                },
            };

        var mockRepo = new Mock<IRepositoryWrapper>();
        var mapperMock = new Mock<IMapper>();

        mockRepo.Setup(x => x.ToponymRepository
            .GetAllAsync(
                It.IsAny<Expression<Func<Toponym, bool>>>(),
                It.IsAny<Func<IQueryable<Toponym>, IIncludableQueryable<Toponym, object>>>()))
            .ReturnsAsync(toponyms);

        mockRepo.Setup(x => x.ToponymRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Toponym, bool>>>(), It.IsAny<Func<IQueryable<Toponym>, IIncludableQueryable<Toponym, object>>>()))
            .ReturnsAsync((Expression<Func<Toponym, bool>> predicate, Func<IQueryable<Toponym>, IIncludableQueryable<Toponym, object>> include) =>
            {
                return toponyms.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.ToponymRepository.Create(It.IsAny<Toponym>()))
        .Returns((Toponym toponym) =>
        {
            toponyms.Add(toponym);
            return toponym;
        });

        mockRepo.Setup(x => x.ToponymRepository.Delete(It.IsAny<Toponym>()))
        .Callback((Toponym? toponym) =>
        {
            toponym = toponyms.FirstOrDefault(x => x.Id == toponym.Id);
            if (toponym != null)
            {
                toponyms.Remove(toponym);
            }
        });

        mockRepo.Setup(x => x.ToponymRepository.Update(It.IsAny<Toponym>()))
            .Returns((Toponym toponym) =>
            {
                var existingToponym = toponyms.Find(u => u.Id == toponym.Id);
                if (existingToponym != null)
                {
                    existingToponym.Community = toponym.Community;
                    existingToponym.AdminRegionOld = toponym.AdminRegionOld;
                    existingToponym.AdminRegionNew = toponym.AdminRegionNew;
                }

                return null;
            });

        mockRepo.Setup(repo => repo.ToponymRepository.GetItemBySpecAsync(
        It.IsAny<ISpecification<Toponym>>()))
        .ReturnsAsync((GetByIdToponymSpec spec) =>
        {
            int id = spec.Id;

            var topns = toponyms.FirstOrDefault(s => s.Id == id);

            return topns;
        });

        mockRepo.Setup(repo => repo.ToponymRepository.GetItemsBySpecAsync(
        It.IsAny<ISpecification<Toponym>>()))
        .ReturnsAsync((GetAllToponymsSpec spec) =>
        {
            return toponyms;
        });

        return mockRepo;
    }
}
