using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Create;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Update;
using Streetcode.DAL.Entities.Sources;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.Sources.SourceLinkCategory
{
    public class UpdateSourceLinkCategoryValidatorTest
    {
        private const int MAXTITLELENGTH = 100;

        private readonly UpdateSourceLinkCategoryCommandValidator _validator;

        public UpdateSourceLinkCategoryValidatorTest()
        {
            _validator = new UpdateSourceLinkCategoryCommandValidator();
        }

        [Theory]
        [InlineData(101)]
        [InlineData(MAXTITLELENGTH + 100)]
        public void UpdateSourceLinkCategoryCommand_TitleIsGreaterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new SourceLinkCategoryDto()
            {
                Title = CreateStringWithSpecificLength(length),
                ImageId = 1
            };
            var request = new UpdateSourceLinkCategoryCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.SourceLinkDto.Title);
        }

        [Fact]
        public void UpdateSourceLinkCategoryCommand_ImaggeIdIsMissed_ShouldHaveErrors()
        {
            // Arrange
            var dto = new SourceLinkCategoryDto()
            {
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH),
                ImageId = 0
            };
            var request = new UpdateSourceLinkCategoryCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.SourceLinkDto.ImageId);
        }

        [Fact]
        public void UpdateSourceLinkCategoryCommand_Valid_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new SourceLinkCategoryDto()
            {
                Id = 1,
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH),
                ImageId = 1
            };
            var request = new UpdateSourceLinkCategoryCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.SourceLinkDto.Title);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.SourceLinkDto.ImageId);
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
