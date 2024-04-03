using FluentAssertions;
using Moq;
using Streetcode.DAL.Entities.Team;
using Streetcode.XUnitTest.Repositories.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.Repositories.Team
{
    public class TeamRepositoryTest
    {
        [Fact]
        public async Task Repository_Create_TeamMember_EqualFirstNames()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetTeamRepositoryMock();
            var repository = mockRepo.Object.TeamRepository;
            var teamMemberToAdd = new TeamMember { Id = 5, FirstName = "John", LastName = "Doew", Description = "Some description", IsMain = true, ImageId = 1 };

            // Act
            var createdTeamMember = repository.Create(teamMemberToAdd);

            // Assert
            Assert.Equal(teamMemberToAdd.FirstName, createdTeamMember.FirstName);
        }

        [Fact]
        public async Task Repository_GetAllTeam_ReturnsAllTeam()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetTeamRepositoryMock();

            // Act
            var resultTask = mockRepo.Object.TeamRepository.GetAllAsync(null, null);
            var result = resultTask.Result;

            // Assert
            result.Count().Should().Be(4);
        }

        [Fact]
        public async Task Repository_TeamUpdate()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetTeamRepositoryMock();
            var repository = mockRepo.Object.TeamRepository;
            var teamMemberToUpdate = new TeamMember { Id = 1, FirstName = "John", LastName = "Doe", Description = "Updated description", IsMain = true, ImageId = 1 };

            // Act
            var updatedUser = repository.Update(teamMemberToUpdate);

            // Assert
            mockRepo.Verify(x => x.TeamRepository.Update(It.IsAny<TeamMember>()), Times.Once);
        }

        [Fact]
        public async Task Repository_DeleteTeam_RemovesFromDatabase()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetTeamRepositoryMock();
            var repository = mockRepo.Object.TeamRepository;
            var teamMemberIdToDelete = 1;

            // Act
            repository.Delete(new TeamMember { Id = teamMemberIdToDelete });

            // Assert
            var deletedUser = await repository.GetFirstOrDefaultAsync(u => u.Id == teamMemberIdToDelete);
            Assert.Null(deletedUser);
        }
    }
}