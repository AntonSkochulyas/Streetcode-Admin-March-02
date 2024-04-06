using FluentAssertions;
using Moq;
using Streetcode.DAL.Entities.Users;
using Streetcode.DAL.Enums;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.Repositories.Users
{
	public class UsersRepositoryTest
	{
        [Fact]
        public async Task Repository_CreateUser()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetUsersRepositoryMock();
            var repository = mockRepo.Object.UserRepository;
            var newUser = new User { Id = 4, Name = "Test", Surname = "User", Email = "test@example.com", Login = "testuser", Password = "test123", Role = UserRole.Moderator };

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
            repository.Delete(new User { Id = userIdToDelete });

            // Assert
            mockRepo.Verify(x => x.UserRepository.Delete(It.Is<User>(u => u.Id == userIdToDelete)), Times.Once);
        }

        [Fact]
        public async Task Repository_DeleteUser_RemovesFromDatabase()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetUsersRepositoryMock();
            var repository = mockRepo.Object.UserRepository;
            var userIdToDelete = 1;

            // Act
            repository.Delete(new User { Id = userIdToDelete });

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
            var userToUpdate = new User { Id = 1, Name = "UpdatedName", Surname = "UpdatedSurname", Email = "updatedmail@gmail.com", Login = "updatedlogin", Password = "updatedpassword", Role = UserRole.Administrator };

            // Act
            var updatedUser = repository.Update(userToUpdate);

            // Assert
            mockRepo.Verify(x => x.UserRepository.Update(It.IsAny<User>()), Times.Once);
        }
    }

}
