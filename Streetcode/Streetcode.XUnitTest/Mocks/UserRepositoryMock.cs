namespace Streetcode.XUnitTest.MediatRTests.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Users;
using Streetcode.DAL.Enums;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    /// <summary>
    /// Mocks user repository.
    /// </summary>
    /// <returns>Returns mocked repository. </returns>
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
}
