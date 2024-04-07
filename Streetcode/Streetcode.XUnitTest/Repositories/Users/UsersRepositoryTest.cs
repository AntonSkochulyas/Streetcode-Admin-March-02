using System;
using AutoMapper;
using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Routing;
using Moq;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Users;
using Streetcode.DAL.Entities.Users;
using Streetcode.DAL.Enums;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Xunit;

namespace Streetcode.XUnitTest.Repositories.Mocks.Users
{
	public class UsersRepositoryTest
	{
        [Fact]
        public async Task Repository_CreateUser()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetUsersRepositoryMock();
            var repository = mockRepo.Object.UserRepository;
            var newUser = new UserAdditionalInfo { Id = 4, Name = "Test", Surname = "User", Email = "test@example.com", Login = "testuser", Password = "test123", Role = UserRole.Moderator };

            // Act
            var createdUser = repository.Create(newUser);

            // Assert
            Assert.NotNull(createdUser);
            Assert.Equal(newUser, createdUser);
        }

        [Fact]
        public async Task Repository_GetAllUsers_ReturnsAllUsers()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetUsersRepositoryMock();

            // Act
            var resultTask = mockRepo.Object.UserRepository.GetAllAsync(null, null);
            var result = resultTask.Result;

            // Assert
            result.Count().Should().Be(3);
            result.Should().ContainSingle(u => u.Name == "John")
                  .And.ContainSingle(u => u.Name == "Maria")
                  .And.ContainSingle(u => u.Name == "Anton");
        }

        [Fact]
        public async Task Repository_DeleteUser()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetUsersRepositoryMock();
            var repository = mockRepo.Object.UserRepository;
            var userIdToDelete = 1; 

            // Act
            repository.Delete(new UserAdditionalInfo { Id = userIdToDelete });

            // Assert
            mockRepo.Verify(x => x.UserRepository.Delete(It.Is<UserAdditionalInfo>(u => u.Id == userIdToDelete)), Times.Once);
        }

        [Fact]
        public async Task Repository_DeleteUser_RemovesFromDatabase()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetUsersRepositoryMock();
            var repository = mockRepo.Object.UserRepository;
            var userIdToDelete = 1;

            // Act
            repository.Delete(new UserAdditionalInfo { Id = userIdToDelete });

            // Assert
            var deletedUser = await repository.GetFirstOrDefaultAsync(u => u.Id == userIdToDelete);
            Assert.Null(deletedUser); 
        }

        [Fact]
        public async Task Repository_UserUpdate()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetUsersRepositoryMock();
            var repository = mockRepo.Object.UserRepository;
            var userToUpdate = new UserAdditionalInfo { Id = 1, Name = "UpdatedName", Surname = "UpdatedSurname", Email = "updatedmail@gmail.com", Login = "updatedlogin", Password = "updatedpassword", Role = UserRole.Administrator };

            // Act
            var updatedUser = repository.Update(userToUpdate);

            // Assert
            mockRepo.Verify(x => x.UserRepository.Update(It.IsAny<UserAdditionalInfo>()), Times.Once);
        }
    }

}
