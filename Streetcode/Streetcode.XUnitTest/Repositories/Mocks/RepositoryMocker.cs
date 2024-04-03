﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Locations;
using Streetcode.DAL.Entities.Team;
using Streetcode.DAL.Entities.Toponyms;
using Streetcode.DAL.Entities.Users;
using Streetcode.DAL.Enums;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Repositories.Interfaces.Locations;
using Streetcode.DAL.Repositories.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
    }
}
