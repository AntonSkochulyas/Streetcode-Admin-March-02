using System.Text;
using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.AdditionalContent.Coordinates.Types;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create;
using Streetcode.BLL.MediatR.Dictionaries.Create;
using Streetcode.XUnitTest.ValidationTests.TestHelperMethods;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.Dictionaries
{
    public class CreateDictionaryItemValidatorTest
    {
        private const int MAXNAMELENGTH = 50;
        private const int MAXDESCRIPTIONLENGTH = 500;

        private readonly CreateDictionaryItemCommandValidator _validator;

        public CreateDictionaryItemValidatorTest()
        {
            _validator = new CreateDictionaryItemCommandValidator();
        }

        [Theory]
        [InlineData(60)]
        [InlineData(MAXNAMELENGTH + 100)]
        public void CreateDictionaryItemCommand_NameLengthIsGreaterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new CreateDictionaryItemDto()
            {
                Word = TestHelper.CreateStringWithSpecificLength(length),
                Description = TestHelper.CreateStringWithSpecificLength(MAXDESCRIPTIONLENGTH)
            };
            var request = new CreateDictionaryItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.CreateDictionaryItemDto.Word);
        }

        [Theory]
        [InlineData(501)]
        [InlineData(MAXDESCRIPTIONLENGTH + 10)]
        public void CreateDictionaryItemCommand_DescriptionLengthIsGreaterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new CreateDictionaryItemDto()
            {
                Word = TestHelper.CreateStringWithSpecificLength(MAXNAMELENGTH),
                Description = TestHelper.CreateStringWithSpecificLength(length)
            };
            var request = new CreateDictionaryItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.CreateDictionaryItemDto.Description);
        }

        [Fact]
        public void CreateDictionaryItemCommand_ValidData_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new CreateDictionaryItemDto()
            {
                Word = TestHelper.CreateStringWithSpecificLength(MAXNAMELENGTH),
                Description = TestHelper.CreateStringWithSpecificLength(MAXDESCRIPTIONLENGTH)
            };
            var request = new CreateDictionaryItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.CreateDictionaryItemDto.Word);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.CreateDictionaryItemDto.Description);
        }
    }
}
