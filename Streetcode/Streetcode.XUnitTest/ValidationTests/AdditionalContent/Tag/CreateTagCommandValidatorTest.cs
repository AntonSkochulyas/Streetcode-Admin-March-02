using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.AdditionalContent.Coordinates.Types;
using Streetcode.BLL.Dto.AdditionalContent.Tag;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create;
using Streetcode.BLL.MediatR.AdditionalContent.Tag.Create;
using Streetcode.XUnitTest.ValidationTests.TestHelperMethods;
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
                Title = TestHelper.CreateStringWithSpecificLength(titleLength)
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
                Title = TestHelper.CreateStringWithSpecificLength(MAXTITLELENGTH)
            };

            var request = new CreateTagCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Tag.Title);
        }
    }
}
