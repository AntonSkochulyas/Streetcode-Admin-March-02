using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.Partners;
using Streetcode.BLL.Dto.Team;
using Streetcode.BLL.MediatR.Team.Create;
using Streetcode.BLL.MediatR.Team.TeamMembersLinks.Create;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.Team.TeamMemberLinks
{
    public class TeamLinkCommandValidator
    {
        private readonly CreateTeamLinkCommandValidator _validator;

        public TeamLinkCommandValidator()
        {
            _validator = new CreateTeamLinkCommandValidator();
        }

        [Theory]
        [InlineData(LogoTypeDto.Instagram)]
        [InlineData(LogoTypeDto.YouTube)]
        [InlineData(LogoTypeDto.Facebook)]
        public void LogoType_Is_Required_Should_Pass(LogoTypeDto logoType)
        {
            // Arrange
            var dto = new TeamMemberLinkDto() { LogoType = logoType };
            var request = new CreateTeamLinkQuery(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.TeamMember.LogoType);
        }

        [Fact]
        public void LogoType_Is_Required_Should_Not_Pass()
        {
            // Arrange
            var dto = new TeamMemberLinkDto() { LogoType = default };
            var request = new CreateTeamLinkQuery(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.TeamMember.LogoType);
        }

        [Theory]
        [InlineData(256)]
        [InlineData(1000)]
        [InlineData(333)]
        public void TargetURL_Max_Length_Should_Not_Pass(int targetURLLength)
        {
            string targetURL = string.Empty;

            for (int i = 0; i < targetURLLength; i++)
            {
                targetURL += "A";
            }

            // Arrange
            var dto = new TeamMemberLinkDto() { TargetUrl = targetURL };
            var request = new CreateTeamLinkQuery(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.TeamMember.TargetUrl);
        }

        [Theory]
        [InlineData(255)]
        [InlineData(1)]
        [InlineData(128)]
        public void TargetURL_Max_Length_Should_Pass(int targetURLLength)
        {
            string targetURL = string.Empty;

            for (int i = 0; i < targetURLLength; i++)
            {
                targetURL += "A";
            }

            // Arrange
            var dto = new TeamMemberLinkDto() { TargetUrl = targetURL };
            var request = new CreateTeamLinkQuery(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.TeamMember.TargetUrl);
        }

        [Fact]
        public void TargetURL_Is_Required_Should_Not_Pass()
        {
            string targetURL = string.Empty;

            // Arrange
            var dto = new TeamMemberLinkDto() { TargetUrl = targetURL };
            var request = new CreateTeamLinkQuery(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.TeamMember.TargetUrl);
        }

        [Fact]
        public void TargetURL_Is_Required_Should_Pass()
        {
            string targetURL = "A";

            // Arrange
            var dto = new TeamMemberLinkDto() { TargetUrl = targetURL };
            var request = new CreateTeamLinkQuery(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.TeamMember.TargetUrl);
        }

        [Fact]
        public void TeamMemberId_Is_Required_Should_Pass()
        {
            // Arrange
            var dto = new TeamMemberLinkDto() { TeamMemberId = 1 };
            var request = new CreateTeamLinkQuery(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.TeamMember.TeamMemberId);
        }

        [Fact]
        public void TeamMemberId_Is_Required_Should_Not_Pass()
        {
            // Arrange
            var dto = new TeamMemberLinkDto() { };
            var request = new CreateTeamLinkQuery(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.TeamMember.TeamMemberId);
        }
    }
}
