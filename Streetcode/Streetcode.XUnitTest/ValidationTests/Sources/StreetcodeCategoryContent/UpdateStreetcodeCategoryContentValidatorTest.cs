using System.Text;
using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Update;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Update;
using Streetcode.XUnitTest.ValidationTests.TestHelperMethods;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.Sources.StreetcodeCategoryContent
{
    public class UpdateStreetcodeCategoryContentValidatorTest
    {
        private const int MAXTEXTLENGTH = 4000;

        private readonly UpdateStreetcodeCategoryContentCommandValidator _validator;

        public UpdateStreetcodeCategoryContentValidatorTest()
        {
            _validator = new UpdateStreetcodeCategoryContentCommandValidator();
        }

        [Theory]
        [InlineData(4001)]
        [InlineData(MAXTEXTLENGTH + 100)]
        public void UpdateStreetcodeCategoryContentCommand_TextIsGreaterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new StreetcodeCategoryContentDto()
            {
                Text = TestHelper.CreateStringWithSpecificLength(length),
                SourceLinkCategoryId = 1,
                StreetcodeId = 1
            };
            var request = new UpdateStreetcodeCategoryContentCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.StreetcodeCategoryContentDto.Text);
        }

        [Fact]
        public void UpdateStreetcodeCategoryContentCommand_SourceLinkIdIsMissed_ShouldHaveErrors()
        {
            // Arrange
            var dto = new StreetcodeCategoryContentDto()
            {
                Text = TestHelper.CreateStringWithSpecificLength(MAXTEXTLENGTH),
                SourceLinkCategoryId = 0,
                StreetcodeId = 1,
            };
            var request = new UpdateStreetcodeCategoryContentCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.StreetcodeCategoryContentDto.SourceLinkCategoryId);
        }

        [Fact]
        public void UpdateStreetcodeCategoryContentCommand_StreetcodeIdIsMissed_ShouldHaveErrors()
        {
            // Arrange
            var dto = new StreetcodeCategoryContentDto()
            {
                Text = TestHelper.CreateStringWithSpecificLength(MAXTEXTLENGTH),
                SourceLinkCategoryId = 1,
                StreetcodeId = 0
            };
            var request = new UpdateStreetcodeCategoryContentCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.StreetcodeCategoryContentDto.StreetcodeId);
        }

        [Fact]
        public void UpdateStreetcodeCategoryContentCommand_Valid_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new StreetcodeCategoryContentDto()
            {
                Text = TestHelper.CreateStringWithSpecificLength(MAXTEXTLENGTH),
                SourceLinkCategoryId = 1,
                StreetcodeId = 1
            };
            var request = new UpdateStreetcodeCategoryContentCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.StreetcodeCategoryContentDto.Text);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.StreetcodeCategoryContentDto.SourceLinkCategoryId);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.StreetcodeCategoryContentDto.StreetcodeId);
        }
    }
}
