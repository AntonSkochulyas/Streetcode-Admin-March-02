using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.InfoBlocks.Articles;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.MediatR.InfoBlocks.Articles.Create;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Create;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.Sources.SourceLinkCategory
{
    public class CreateSourceLinkCategoryValidatorTest
    {
        private const int MAXTITLELENGTH = 100;

        private readonly CreateSourceLinkCategoryCommandValidator _validator;

        public CreateSourceLinkCategoryValidatorTest()
        {
            _validator = new CreateSourceLinkCategoryCommandValidator();
        }

        [Theory]
        [InlineData(101)]
        [InlineData(MAXTITLELENGTH + 100)]
        public void CreateSourceLinkCategoryCommand_TitleIsGreaterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new CreateSourceLinkDto()
            {
                Title = CreateStringWithSpecificLength(length),
                ImageId = 1
            };
            var request = new CreateSourceLinkCategoryCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.SourceLinkCategoryContentDto.Title);
        }

        [Fact]
        public void CreateSourceLinkCategoryCommand_ImaggeIdIsMissed_ShouldHaveErrors()
        {
            // Arrange
            var dto = new CreateSourceLinkDto()
            {
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH),
                ImageId = 0
            };
            var request = new CreateSourceLinkCategoryCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.SourceLinkCategoryContentDto.ImageId);
        }

        [Fact]
        public void CreateSourceLinkCategoryCommand_Valid_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new CreateSourceLinkDto()
            {
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH),
                ImageId = 1
            };
            var request = new CreateSourceLinkCategoryCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.SourceLinkCategoryContentDto.Title);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.SourceLinkCategoryContentDto.ImageId);
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
