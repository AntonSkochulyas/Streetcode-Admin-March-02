using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.Partners;
using Streetcode.BLL.MediatR.Partners.Create;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.Partners
{
    public class PartnerValidatorTest
    {
        private readonly CreatePartnerCommandValidator _validator;

        public PartnerValidatorTest()
        {
            _validator = new CreatePartnerCommandValidator();
        }

        [Fact]
        public void Title_Is_Required_Should_Not_Pass()
        {
            // Arrange
            var dto = new CreatePartnerDto() { };
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.NewPartner.Title);
        }

        [Fact]
        public void Title_Is_Required_Should_Pass()
        {
            // Arrange
            var dto = new CreatePartnerDto() { Title = "A" };
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.NewPartner.Title);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        [InlineData(49)]
        public void Title_Max_Length_Should_Pass(int titleLength)
        {
            // Arrange
            string title = string.Empty;

            for (int i = 0; i < titleLength; i++)
            {
                title += "A";
            }

            var dto = new CreatePartnerDto() { Title = title };
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.NewPartner.Title);
        }

        [Theory]
        [InlineData(254)]
        [InlineData(101)]
        [InlineData(333)]
        public void Title_Max_Length_Should_Not_Pass(int titleLength)
        {
            // Arrange
            string title = string.Empty;

            for (int i = 0; i < titleLength; i++)
            {
                title += "A";
            }

            var dto = new CreatePartnerDto() { Title = title };
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.NewPartner.Title);
        }

        [Fact]
        public void LogoId_Is_Required_Should_Not_Pass()
        {
            // Arrange
            var dto = new CreatePartnerDto() { };
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.NewPartner.LogoId);
        }

        [Fact]
        public void LogoId_Is_Required_Should_Pass()
        {
            // Arrange
            var dto = new CreatePartnerDto() { LogoId = 1 };
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.NewPartner.LogoId);
        }

        [Fact]
        public void IsKeyPartner_Is_Required_Should_Not_Pass()
        {
            // Arrange
            var dto = new CreatePartnerDto() { };
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.NewPartner.IsKeyPartner);
        }

        [Fact]
        public void IsKeyPartner_Is_Required_Should_Pass()
        {
            // Arrange
            var dto = new CreatePartnerDto() { IsKeyPartner = true };
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.NewPartner.IsKeyPartner);
        }


        [Fact]
        public void IsVisibleEverywhere_Is_Required_Should_Not_Pass()
        {
            // Arrange
            var dto = new CreatePartnerDto() { };
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.NewPartner.IsVisibleEverywhere);
        }

        [Fact]
        public void IsVisibleEverywhere_Is_Required_Should_Pass()
        {
            // Arrange
            var dto = new CreatePartnerDto() { IsVisibleEverywhere = true };
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.NewPartner.IsVisibleEverywhere);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        [InlineData(49)]
        public void TargetURL_Max_Length_Should_Pass(int targetURLLength)
        {
            // Arrange
            string targetURL = string.Empty;

            for (int i = 0; i < targetURLLength; i++)
            {
                targetURL += "A";
            }

            var dto = new CreatePartnerDto() { Title = targetURL };
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.NewPartner.TargetUrl);
        }

        [Theory]
        [InlineData(254)]
        [InlineData(101)]
        [InlineData(333)]
        public void TargetURL_Max_Length_Should_Not_Pass(int targetURLength)
        {
            // Arrange
            string targetURL = string.Empty;

            for (int i = 0; i < targetURLength; i++)
            {
                targetURL += "A";
            }

            var dto = new CreatePartnerDto() { Title = targetURL };
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.NewPartner.Title);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        [InlineData(49)]
        public void URLTitle_Max_Length_Should_Pass(int urlTitleLength)
        {
            // Arrange
            string urlTitle = string.Empty;

            for (int i = 0; i < urlTitleLength; i++)
            {
                urlTitle += "A";
            }

            var dto = new CreatePartnerDto() { UrlTitle = urlTitle };
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.NewPartner.UrlTitle);
        }

        [Theory]
        [InlineData(25345)]
        [InlineData(256)]
        [InlineData(333)]
        public void URLTitle_Max_Length_Should_Not_Pass(int urlTitleLength)
        {
            // Arrange
            string urlTitle = string.Empty;

            for (int i = 0; i < urlTitleLength; i++)
            {
                urlTitle += "A";
            }

            var dto = new CreatePartnerDto() { UrlTitle = urlTitle };
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.NewPartner.UrlTitle);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        [InlineData(49)]
        public void Description_Max_Length_Should_Pass(int descriptionLength)
        {
            // Arrange
            string description = string.Empty;

            for (int i = 0; i < descriptionLength; i++)
            {
                description += "A";
            }

            var dto = new CreatePartnerDto() { Description = description };
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.NewPartner.Description);
        }

        [Theory]
        [InlineData(25345)]
        [InlineData(1355)]
        [InlineData(650)]
        public void Description_Max_Length_Should_Not_Pass(int descriptionLength)
        {
            // Arrange
            string description = string.Empty;

            for (int i = 0; i < descriptionLength; i++)
            {
                description += "A";
            }

            var dto = new CreatePartnerDto() { Description = description };
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.NewPartner.Description);
        }
    }
}
