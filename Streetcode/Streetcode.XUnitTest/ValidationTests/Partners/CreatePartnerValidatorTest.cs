using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.Partners;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.MediatR.Partners.Create;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Create;
using Streetcode.XUnitTest.ValidationTests.TestHelperMethods;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.Partners
{
    public class CreatePartnerValidatorTest
    {
        private const int TITLEMAXLENGTH = 100;
        private const int TARGETURLMAXLENGTH = 200;
        private const int URLTITLEMAXLENGTH = 255;
        private const int DESCRIPTIONMAXLENGTH = 450;

        private readonly CreatePartnerCommandValidator _validator;

        public CreatePartnerValidatorTest()
        {
            _validator = new CreatePartnerCommandValidator();
        }

        [Theory]
        [InlineData(TITLEMAXLENGTH + 1)]
        [InlineData(TITLEMAXLENGTH + 100)]
        public void CreatePartnerCommand_TitleIsGreaterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = GenerateCreatePartnerDto(titleLength: length);
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.NewPartner.Title);
        }

        [Theory]
        [InlineData(TARGETURLMAXLENGTH + 1)]
        [InlineData(TARGETURLMAXLENGTH + 100)]
        public void CreatePartnerCommand_TargetUrlIsGreaterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = GenerateCreatePartnerDto(targetUrlLength: length);
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.NewPartner.TargetUrl);
        }

        [Theory]
        [InlineData(URLTITLEMAXLENGTH + 1)]
        [InlineData(URLTITLEMAXLENGTH + 100)]
        public void CreatePartnerCommand_UrlTitleIsGreaterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = GenerateCreatePartnerDto(urlTileLength: length);
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.NewPartner.UrlTitle);
        }

        [Theory]
        [InlineData(DESCRIPTIONMAXLENGTH + 1)]
        [InlineData(DESCRIPTIONMAXLENGTH + 100)]
        public void CreatePartnerCommand_DescriptionIsGreaterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = GenerateCreatePartnerDto(desciptionLength: length);
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.NewPartner.Description);
        }

        [Fact]
        public void CreatePartnerCommand_ValidData_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = GenerateCreatePartnerDto();
            var request = new CreatePartnerCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.NewPartner.Title);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.NewPartner.TargetUrl);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.NewPartner.UrlTitle);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.NewPartner.Description);
        }

        private CreatePartnerDto GenerateCreatePartnerDto(int titleLength = TITLEMAXLENGTH,
            int targetUrlLength = TARGETURLMAXLENGTH, int urlTileLength = URLTITLEMAXLENGTH,
            int desciptionLength = DESCRIPTIONMAXLENGTH)
        {
            return new CreatePartnerDto
            {
                Title = TestHelper.CreateStringWithSpecificLength(titleLength),
                TargetUrl = TestHelper.CreateStringWithSpecificLength(targetUrlLength),
                UrlTitle = TestHelper.CreateStringWithSpecificLength(urlTileLength),
                Description = TestHelper.CreateStringWithSpecificLength(desciptionLength),
            };
        }
    }
}
