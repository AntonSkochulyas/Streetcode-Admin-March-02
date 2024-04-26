using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Create;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Create;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.Sources.StreetcodeCategoryContent
{
    public class CreateStreetcodeCategoryContentValidatorTest
    {
        private const int MAXTEXTLENGTH = 4000;

        private readonly CreateStreetcodeCategoryContentCommandValidator _validator;

        public CreateStreetcodeCategoryContentValidatorTest()
        {
            _validator = new CreateStreetcodeCategoryContentCommandValidator();
        }

        [Theory]
        [InlineData(4001)]
        [InlineData(MAXTEXTLENGTH + 100)]
        public void CreateStreetcodeCategoryContentCommand_TextIsGreaterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new StreetcodeCategoryContentDto()
            {
                Text = CreateStringWithSpecificLength(length),
                SourceLinkCategoryId = 1,
                StreetcodeId = 1
            };
            var request = new CreateStreetcodeCategoryContentCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.StreetcodeCategoryContentDto.Text);
        }

        [Fact]
        public void CreateStreetcodeCategoryContentCommand_SourceLinkIdIsMissed_ShouldHaveErrors()
        {
            // Arrange
            var dto = new StreetcodeCategoryContentDto()
            {
                Text = CreateStringWithSpecificLength(MAXTEXTLENGTH),
                SourceLinkCategoryId = 0,
                StreetcodeId = 1,
            };
            var request = new CreateStreetcodeCategoryContentCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.StreetcodeCategoryContentDto.SourceLinkCategoryId);
        }

        [Fact]
        public void CreateStreetcodeCategoryContentCommand_StreetcodeIdIsMissed_ShouldHaveErrors()
        {
            // Arrange
            var dto = new StreetcodeCategoryContentDto()
            {
                Text = CreateStringWithSpecificLength(MAXTEXTLENGTH),
                SourceLinkCategoryId = 1,
                StreetcodeId = 0
            };
            var request = new CreateStreetcodeCategoryContentCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.StreetcodeCategoryContentDto.StreetcodeId);
        }

        [Fact]
        public void CreateStreetcodeCategoryContentCommand_Valid_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new StreetcodeCategoryContentDto()
            {
                Text = CreateStringWithSpecificLength(MAXTEXTLENGTH),
                SourceLinkCategoryId = 1,
                StreetcodeId = 1
            };
            var request = new CreateStreetcodeCategoryContentCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.StreetcodeCategoryContentDto.Text);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.StreetcodeCategoryContentDto.SourceLinkCategoryId);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.StreetcodeCategoryContentDto.StreetcodeId);
        }

        private string CreateStringWithSpecificLength(int length)
        {
            string character = "A";
            var result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(character);
            }

            return result.ToString();
        }
    }
}
