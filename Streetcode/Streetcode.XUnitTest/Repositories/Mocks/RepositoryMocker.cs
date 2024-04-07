using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Partners;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Entities.Team;
using Streetcode.DAL.Entities.Timeline;
using Streetcode.DAL.Entities.Toponyms;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

namespace Streetcode.XUnitTest.Repositories.Mocks
{
    internal class RepositoryMocker
    {
        public static Mock<IRepositoryWrapper> GetToponymsRepositoryMock()
        {
            var toponyms = new List<Toponym>()
            {
                new Toponym() { Id = 1, Community = "First community", AdminRegionNew = "First region new", AdminRegionOld = "First region old" },
                new Toponym() { Id = 2, Community = "Second community", AdminRegionNew = "Second region new", AdminRegionOld = "Second region old" },
                new Toponym() { Id = 3, Community = "Third community", AdminRegionNew = "Third region new", AdminRegionOld = "Third region old" },
            };

            var mockRepo = new Mock<IRepositoryWrapper>();

            mockRepo.Setup(x => x.ToponymRepository
                .GetAllAsync(
                    It.IsAny<Expression<Func<Toponym, bool>>>(),
                    It.IsAny<Func<IQueryable<Toponym>, IIncludableQueryable<Toponym, object>>>()))
                .ReturnsAsync(toponyms);

            mockRepo.Setup(x => x.ToponymRepository.Create(It.IsAny<Toponym>()))
            .Returns((Toponym toponym) =>
            {
                toponyms.Add(toponym);
                return toponym;
            });

            mockRepo.Setup(x => x.ToponymRepository.Delete(It.IsAny<Toponym>()))
            .Callback((Toponym toponym) =>
            {
                toponyms.Remove(toponym);
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

            return mockRepo;
        }

        public static Mock<IRepositoryWrapper> GetTeamRepositoryMock()
        {
            var teams = new List<TeamMember>()
            {
                 new TeamMember { Id = 1, FirstName = "John", LastName = "Doe", Description = "description1", IsMain = true, ImageId = 1 },
                 new TeamMember { Id = 2, FirstName = "Jane", LastName = "Mur", Description = "description2", IsMain = false, ImageId = 2 },
                 new TeamMember { Id = 3, FirstName = "Mila", LastName = "Lyubow", Description = "description3", IsMain = true, ImageId = 3 },
                 new TeamMember { Id = 4, FirstName = "Orest", LastName = "Fifa", Description = "description4", IsMain = false, ImageId = 2 }
            };

            var mockRepo = new Mock<IRepositoryWrapper>();

            mockRepo.Setup(x => x.TeamRepository
                .GetAllAsync(
                    It.IsAny<Expression<Func<TeamMember, bool>>>(),
                    It.IsAny<Func<IQueryable<TeamMember>, IIncludableQueryable<TeamMember, object>>>()))
                .ReturnsAsync(teams);

            mockRepo.Setup(x => x.TeamRepository.Create(It.IsAny<TeamMember>()))
                 .Returns((TeamMember teamMember) =>
                 {
                     teams.Add(teamMember);
                     return teamMember;
                 });
            mockRepo.Setup(x => x.TeamRepository.Delete(It.IsAny<TeamMember>()))
            .Callback((TeamMember teamMember) =>
            {
                teams.Remove(teamMember);
            });

            return mockRepo;
        }

        public static Mock<IRepositoryWrapper> GetTimelineRepositoryMock()
        {
            var timelineItems = new List<TimelineItem>()
        {
            new TimelineItem { Id = 1, Date = DateTime.Now, Title = "First Event", Description = "Description of the first event" },
            new TimelineItem { Id = 2, Date = DateTime.Now.AddDays(1), Title = "Second Event", Description = "Description of the second event" },
        };

            var mockRepo = new Mock<IRepositoryWrapper>();

            mockRepo.Setup(x => x.TimelineRepository
                .GetAllAsync(
                    It.IsAny<Expression<Func<TimelineItem, bool>>>(),
                    It.IsAny<Func<IQueryable<TimelineItem>, IIncludableQueryable<TimelineItem, object>>>()))
                .ReturnsAsync(timelineItems);

            mockRepo.Setup(x => x.TimelineRepository.Create(It.IsAny<TimelineItem>()))
                 .Returns((TimelineItem timelineItem) =>
                 {
                     timelineItems.Add(timelineItem);
                     return timelineItem;
                 });

            mockRepo.Setup(x => x.TimelineRepository.Delete(It.IsAny<TimelineItem>()))
            .Callback((TimelineItem timelineItem) =>
            {
                timelineItems.Remove(timelineItem);
            });

            return mockRepo;
        }

        public static Mock<IRepositoryWrapper> GetStreetcodeRepositoryMock()
        {
            var streetcodeContents = new List<StreetcodeContent>()
        {
            new StreetcodeContent { Id = 1, Title = "First Streetcode", TransliterationUrl = "first-streetcode", DateString = "2024-04-05" },
            new StreetcodeContent { Id = 2, Title = "Second Streetcode", TransliterationUrl = "second-streetcode", DateString = "2024-04-06" },
        };

            var mockRepo = new Mock<IRepositoryWrapper>();

            mockRepo.Setup(x => x.StreetcodeRepository
                .GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ReturnsAsync(streetcodeContents);

            mockRepo.Setup(x => x.StreetcodeRepository.Create(It.IsAny<StreetcodeContent>()))
                .Returns((StreetcodeContent streetcode) =>
                {
                    streetcodeContents.Add(streetcode);
                    return streetcode;
                });

            mockRepo.Setup(x => x.StreetcodeRepository.Delete(It.IsAny<StreetcodeContent>()))
                .Callback((StreetcodeContent streetcode) =>
                {
                    streetcodeContents.Remove(streetcode);
                });

            return mockRepo;
        }

        public static Mock<IRepositoryWrapper> GetPartnersRepositoryMock()
        {
            var partners = new List<Partner>()
            {
                new Partner() { Id = 1, Title = "First partner" },
                new Partner() { Id = 2, Title = "Second partner" },
                new Partner() { Id = 3, Title = "Third partner" },
                new Partner() { Id = 4, Title = "Fourth partner" },
            };

            var mockRepo = new Mock<IRepositoryWrapper>();

            mockRepo.Setup(x => x.PartnersRepository
                .GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partners);

            mockRepo.Setup(x => x.PartnersRepository.Create(It.IsAny<Partner>()))
                .Returns((Partner partner) =>
                {
                    partners.Add(partner);
                    return partner;
                });

            mockRepo.Setup(x => x.PartnersRepository.Delete(It.IsAny<Partner>()))
                .Callback((Partner partner) =>
                {
                    partners.Remove(partner);
                });

            return mockRepo;
        }
    }
}
