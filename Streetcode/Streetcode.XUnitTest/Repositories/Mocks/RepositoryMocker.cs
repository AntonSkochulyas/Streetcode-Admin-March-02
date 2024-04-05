using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Entities.Partners;
using Streetcode.DAL.Entities.Sources;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Entities.Team;
using Streetcode.DAL.Entities.Timeline;
using Streetcode.DAL.Entities.Toponyms;
using Streetcode.DAL.Entities.News;
using Streetcode.DAL.Entities.Users;
using Streetcode.DAL.Enums;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;
using Streetcode.DAL.Entities.AdditionalContent.Coordinates;
using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;
using Streetcode.DAL.Entities.AdditionalContent;

namespace Streetcode.XUnitTest.Repositories.Mocks
{
    internal class RepositoryMocker
    {
        public static Mock<IRepositoryWrapper> GetUsersRepositoryMock()
        {
            var users = new List<User>()
            {
                new User() { Id = 1, Name = "John", Surname = "Smith", Email = "mail1@gmail.com", Login = "john1", Password = "smith1", Role = UserRole.Administrator },
                new User() { Id = 2, Name = "Maria", Surname = "Low", Email = "mail2@gmail.com", Login = "maria1", Password = "low1", Role = UserRole.MainAdministrator },
                new User() { Id = 3, Name = "Anton", Surname = "Shults", Email = "mail3@gmail.com", Login = "anton1", Password = "shults1", Role = UserRole.MainAdministrator },
            };

            var mockRepo = new Mock<IRepositoryWrapper>();

            mockRepo.Setup(x => x.UserRepository
                .GetAllAsync(
                    It.IsAny<Expression<Func<User, bool>>>(),
                    It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>()))
                .ReturnsAsync(users);

            mockRepo.Setup(x => x.UserRepository.Create(It.IsAny<User>()))
            .Returns((User user) =>
            {
                users.Add(user);
                return user;
            });

            mockRepo.Setup(x => x.UserRepository.Delete(It.IsAny<User>()))
            .Callback((User user) =>
            {
                users.Remove(user);
            });

            mockRepo.Setup(x => x.UserRepository.Update(It.IsAny<User>()))
                .Returns((User user) =>
                {
                    var existingUser = users.Find(u => u.Id == user.Id);
                    if (existingUser != null)
                    {
                        existingUser.Name = user.Name;
                        existingUser.Surname = user.Surname;
                        existingUser.Email = user.Email;
                        existingUser.Login = user.Login;
                        existingUser.Password = user.Password;
                        existingUser.Role = user.Role;
                    }

                    return null;
                });

            return mockRepo;
        }

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

        public static Mock<IRepositoryWrapper> GetNewsRepositoryMock()
        {
            var news = new List<DAL.Entities.News.News>()
            {
                new DAL.Entities.News.News() { Id = 1, Title = "First title", Text = "First text", CreationDate = new DateTime(2020, 1, 1, 1, 1, 1, DateTimeKind.Utc), URL = "Url1", ImageId = 1 },
                new DAL.Entities.News.News() { Id = 2, Title = "Second title", Text = "Second text", CreationDate = new DateTime(2020, 2, 2, 2, 2, 2, DateTimeKind.Utc), URL = "Url2", ImageId = 2 },
                new DAL.Entities.News.News() { Id = 3, Title = "Third title", Text = "Third text", CreationDate = new DateTime(2020, 3, 3, 3, 3, 3, DateTimeKind.Utc), URL = "Url3", ImageId = 3 },
            };

            var mockRepo = new Mock<IRepositoryWrapper>();

            mockRepo.Setup(x => x.NewsRepository
                .GetAllAsync(
                    It.IsAny<Expression<Func<DAL.Entities.News.News, bool>>>(),
                    It.IsAny<Func<IQueryable<DAL.Entities.News.News>, IIncludableQueryable<DAL.Entities.News.News, object>>>()))
                .ReturnsAsync(news);

            mockRepo.Setup(x => x.NewsRepository.Create(It.IsAny<DAL.Entities.News.News>()))
            .Returns((DAL.Entities.News.News newsItem) =>
            {
                news.Add(newsItem);
                return newsItem;
            });

            mockRepo.Setup(x => x.NewsRepository.Delete(It.IsAny<DAL.Entities.News.News>()))
            .Callback((DAL.Entities.News.News newsItem) =>
            {
                news.Remove(newsItem);
            });

            mockRepo.Setup(x => x.NewsRepository.Update(It.IsAny<DAL.Entities.News.News>()))
                .Returns((DAL.Entities.News.News newsItem) =>
                {
                    var existingNewsItem = news.Find(n => n.Id == newsItem.Id);
                    if (existingNewsItem != null)
                    {
                        existingNewsItem.Title = newsItem.Title;
                        existingNewsItem.Text = newsItem.Text;
                        existingNewsItem.URL = newsItem.URL;
                        existingNewsItem.CreationDate = newsItem.CreationDate;
                        existingNewsItem.ImageId = newsItem.ImageId;
                        existingNewsItem.Image = newsItem.Image;
                    }

                    return null;
                });

            return mockRepo;
        }

        public static Mock<IRepositoryWrapper> GetSourceCategoryRepositoryMock()
        {
            var sourceLinkCategories = new List<SourceLinkCategory>()
            {
                new SourceLinkCategory() { Id = 1, Title = "First title", Image = new Image() { Id = 1, Base64 = "TestBlob1", }},
                new SourceLinkCategory() { Id = 2, Title = "Second title", Image = new Image() { Id = 2, Base64 = "TestBlob2", }},
                new SourceLinkCategory() { Id = 3, Title = "Third title", Image = new Image() { Id = 3, Base64 = "TestBlob3", }},
            };

            var mockRepo = new Mock<IRepositoryWrapper>();

            mockRepo.Setup(x => x.SourceCategoryRepository
                .GetAllAsync(
                    It.IsAny<Expression<Func<SourceLinkCategory, bool>>>(),
                    It.IsAny<Func<IQueryable<SourceLinkCategory>, IIncludableQueryable<SourceLinkCategory, object>>>()))
                .ReturnsAsync(sourceLinkCategories);

            mockRepo.Setup(x => x.SourceCategoryRepository.Create(It.IsAny<SourceLinkCategory>()))
            .Returns((SourceLinkCategory sourceLinkCategory) =>
            {
                sourceLinkCategories.Add(sourceLinkCategory);
                return sourceLinkCategory;
            });

            mockRepo.Setup(x => x.SourceCategoryRepository.Delete(It.IsAny<SourceLinkCategory>()))
            .Callback((SourceLinkCategory sourceLinkCategory) =>
            {
                sourceLinkCategories.Remove(sourceLinkCategory);
            });

            mockRepo.Setup(x => x.SourceCategoryRepository.Update(It.IsAny<SourceLinkCategory>()))
                .Returns((SourceLinkCategory sourceLinkCategory) =>
                {
                    var existingSourceLinkCategory = sourceLinkCategories.Find(s => s.Id == sourceLinkCategory.Id);
                    if (existingSourceLinkCategory != null)
                    {
                        existingSourceLinkCategory.Title = sourceLinkCategory.Title;
                        existingSourceLinkCategory.Image = sourceLinkCategory.Image;
                        existingSourceLinkCategory.ImageId = sourceLinkCategory.ImageId;
                    }

                    return null;
                });

            return mockRepo;
        }

        public static Mock<IRepositoryWrapper> GetStreetcodeCategoryContentRepositoryMock()
        {
            var streetcodeCategoryContents = new List<StreetcodeCategoryContent>()
            {
                new StreetcodeCategoryContent() { SourceLinkCategoryId = 1, StreetcodeId = 1, Text = "Test1" },
                new StreetcodeCategoryContent() { SourceLinkCategoryId = 2, StreetcodeId = 1, Text = "Test2" },
            };

            var mockRepo = new Mock<IRepositoryWrapper>();

            mockRepo.Setup(x => x.StreetcodeCategoryContentRepository
                .GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodeCategoryContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeCategoryContent>, IIncludableQueryable<StreetcodeCategoryContent, object>>>()))
                .ReturnsAsync(streetcodeCategoryContents);

            mockRepo.Setup(x => x.StreetcodeCategoryContentRepository.Create(It.IsAny<StreetcodeCategoryContent>()))
            .Returns((StreetcodeCategoryContent streetcodeCategoryContent) =>
            {
                streetcodeCategoryContents.Add(streetcodeCategoryContent);
                return streetcodeCategoryContent;
            });

            mockRepo.Setup(x => x.StreetcodeCategoryContentRepository.Delete(It.IsAny<StreetcodeCategoryContent>()))
            .Callback((StreetcodeCategoryContent streetcodeCategoryContent) =>
            {
                streetcodeCategoryContents.Remove(streetcodeCategoryContent);
            });

            mockRepo.Setup(x => x.StreetcodeCategoryContentRepository.Update(It.IsAny<StreetcodeCategoryContent>()))
                .Returns((StreetcodeCategoryContent streetcodeCategoryContent) =>
                {
                    var existingStreetcodeCategoryContent = streetcodeCategoryContents.Find(
                        s => s.StreetcodeId == streetcodeCategoryContent.StreetcodeId &&
                        s.SourceLinkCategoryId == streetcodeCategoryContent.SourceLinkCategoryId);
                    if (existingStreetcodeCategoryContent != null)
                    {
                        existingStreetcodeCategoryContent.Text = streetcodeCategoryContent.Text;
                        existingStreetcodeCategoryContent.Streetcode = streetcodeCategoryContent.Streetcode;
                    }

                    return null;
                });

            return mockRepo;
        }

        public static Mock<IRepositoryWrapper> GetStreetcodeCoordinateRepositoryMock()
        {
            List<StreetcodeCoordinate> coordinates = new List<StreetcodeCoordinate>
            {
                new StreetcodeCoordinate
                {
                    Id = 1,
                    StreetcodeId = 1,
                    Longtitude = 1,
                    Latitude = 2
                },
                new StreetcodeCoordinate
                {
                    Id = 2,
                    StreetcodeId = 1,
                    Longtitude = 14,
                    Latitude = 3
                },
            };

            var mockRepo = new Mock<IRepositoryWrapper>();

            mockRepo.Setup(x => x.StreetcodeCoordinateRepository
                .GetAllAsync(
                    It.IsAny<Expression<Func<StreetcodeCoordinate, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeCoordinate>, IIncludableQueryable<StreetcodeCoordinate, object>>>()))
                .ReturnsAsync(coordinates);

            mockRepo.Setup(x => x.StreetcodeCoordinateRepository.Create(It.IsAny<StreetcodeCoordinate>()))
            .Returns((StreetcodeCoordinate coordinate) =>
            {
                coordinates.Add(coordinate);
                return coordinate;
            });

            mockRepo.Setup(x => x.StreetcodeCoordinateRepository.Delete(It.IsAny<StreetcodeCoordinate>()))
            .Callback((StreetcodeCoordinate streetcodeCategoryContent) =>
            {
                coordinates.Remove(streetcodeCategoryContent);
            });

            mockRepo.Setup(x => x.StreetcodeCoordinateRepository.Update(It.IsAny<StreetcodeCoordinate>()))
                .Returns((StreetcodeCoordinate streetcodeCoordinate) =>
                {
                    var existingStreetcodeCoordinate = coordinates.Find(
                        c => c.Id == streetcodeCoordinate.Id);
                    if (existingStreetcodeCoordinate != null)
                    {
                        existingStreetcodeCoordinate.Latitude = streetcodeCoordinate.Latitude;
                        existingStreetcodeCoordinate.Streetcode = streetcodeCoordinate.Streetcode;
                        existingStreetcodeCoordinate.Longtitude = streetcodeCoordinate.Longtitude;
                        existingStreetcodeCoordinate.StatisticRecord = streetcodeCoordinate.StatisticRecord;
                        existingStreetcodeCoordinate.StreetcodeId = streetcodeCoordinate.StreetcodeId;
                    }

                    return null;
                });

            return mockRepo;
        }

        public static Mock<IRepositoryWrapper> GetSubtitleRepositoryMock()
        {
            var subtitles = new List<Subtitle>()
            {
                new Subtitle
                {
                    StreetcodeId = 1,
                    Id = 1,
                    SubtitleText = "Test1",
                },
                new Subtitle
                {
                    StreetcodeId = 1,
                    Id = 2,
                    SubtitleText = "Test2",
                },
                new Subtitle
                {
                    StreetcodeId = 1,
                    Id = 3,
                    SubtitleText = "Test3",
                },
            };

            var mockRepo = new Mock<IRepositoryWrapper>();

            mockRepo.Setup(x => x.SubtitleRepository
                .GetAllAsync(
                    It.IsAny<Expression<Func<Subtitle, bool>>>(),
                    It.IsAny<Func<IQueryable<Subtitle>, IIncludableQueryable<Subtitle, object>>>()))
                .ReturnsAsync(subtitles);

            mockRepo.Setup(x => x.SubtitleRepository.Create(It.IsAny<Subtitle>()))
            .Returns((Subtitle subtitle) =>
            {
                subtitles.Add(subtitle);
                return subtitle;
            });

            mockRepo.Setup(x => x.SubtitleRepository.Delete(It.IsAny<Subtitle>()))
            .Callback((Subtitle subtitle) =>
            {
                subtitles.Remove(subtitle);
            });

            mockRepo.Setup(x => x.SubtitleRepository.Update(It.IsAny<Subtitle>()))
                .Returns((Subtitle subtitle) =>
                {
                    var existingSubtitle = subtitles.Find(
                        c => c.Id == subtitle.Id);
                    if (existingSubtitle != null)
                    {
                        existingSubtitle.StreetcodeId = subtitle.StreetcodeId;
                        existingSubtitle.SubtitleText = subtitle.SubtitleText;
                        existingSubtitle.Streetcode = subtitle.Streetcode;
                    }

                    return null;
                });

            return mockRepo;
        }

        public static Mock<IRepositoryWrapper> GetTagRepositoryMock()
        {
            var tags = new List<Tag>
            {
                new Tag
                {
                    Id = 1,
                    Title = "Test",
                },
                new Tag
                {
                    Id = 2,
                    Title = "Test",
                },
            };

            var mockRepo = new Mock<IRepositoryWrapper>();

            mockRepo.Setup(x => x.TagRepository
                .GetAllAsync(
                    It.IsAny<Expression<Func<Tag, bool>>>(),
                    It.IsAny<Func<IQueryable<Tag>, IIncludableQueryable<Tag, object>>>()))
                .ReturnsAsync(tags);

            mockRepo.Setup(x => x.TagRepository.Create(It.IsAny<Tag>()))
            .Returns((Tag tag) =>
            {
                tags.Add(tag);
                return tag;
            });

            mockRepo.Setup(x => x.TagRepository.Delete(It.IsAny<Tag>()))
            .Callback((Tag tag) =>
            {
                tags.Remove(tag);
            });

            mockRepo.Setup(x => x.TagRepository.Update(It.IsAny<Tag>()))
                .Returns((Tag tag) =>
                {
                    var existingTag = tags.Find(
                        t => t.Id == tag.Id);
                    if (existingTag != null)
                    {
                        existingTag.Title = tag.Title;
                        existingTag.StreetcodeTagIndices = tag.StreetcodeTagIndices;
                        existingTag.Streetcodes = tag.Streetcodes;
                    }

                    return null;
                });

            return mockRepo;
        }
    }
}
