using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.AdditionalContent.Coordinates.Types;
using Streetcode.BLL.Dto.AdditionalContent.Tag;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create;
using Streetcode.BLL.MediatR.AdditionalContent.Tag.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.AdditionalContent.Tag
{
    public class CreateTagCommandValidatorTest
    {
        private const int MAXTITLELENGTH = 50;
        private readonly CreateTagCommandValidator _validator;

        public CreateTagCommandValidatorTest()
        {
            _validator = new CreateTagCommandValidator();
        }

        [Theory]
        [InlineData(150)]
        [InlineData(51)]
        public void CreateTagCommand_TitleLengthIsGreaterThanAllowed_ShouldHaveErrors(int titleLength)
        {
            // Arrange
            var dto = new CreateTagDto()
            {
                Title = CreateStringWithSpecificLength(titleLength)
            };

            var request = new CreateTagCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Tag.Title);
        }

        [Fact]
        public void CreateTagCommand_TitleLengthIsValid_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new CreateTagDto()
            {
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH)
            };

            var request = new CreateTagCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Tag.Title);
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
