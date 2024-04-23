using System.Text;
using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.AdditionalContent.Coordinates.Types;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create;
using Streetcode.BLL.MediatR.Dictionaries.Create;
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
                Word = CreateStringWithSpecificLength(length),
                Description = CreateStringWithSpecificLength(MAXDESCRIPTIONLENGTH)
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
                Word = CreateStringWithSpecificLength(MAXNAMELENGTH),
                Description = CreateStringWithSpecificLength(length)
            };
            var request = new CreateDictionaryItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.CreateDictionaryItemDto.Description);
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
