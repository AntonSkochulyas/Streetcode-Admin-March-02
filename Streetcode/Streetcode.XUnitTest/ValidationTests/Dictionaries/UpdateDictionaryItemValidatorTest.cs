using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.BLL.MediatR.Dictionaries.Update;
using Streetcode.XUnitTest.ValidationTests.TestHelperMethods;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.Dictionaries
{
    public class UpdateDictionaryItemValidatorTest
    {
        private const int MAXNAMELENGTH = 50;
        private const int MAXDESCRIPTIONLENGTH = 500;

        private readonly UpdateDictionaryItemCommandValidator _validator;

        public UpdateDictionaryItemValidatorTest()
        {
            _validator = new UpdateDictionaryItemCommandValidator();
        }

        [Theory]
        [InlineData(60)]
        [InlineData(MAXNAMELENGTH + 100)]
        public void UpdateDictionaryItemCommand_NameLengthIsGreaterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new DictionaryItemDto()
            {
                Id = 1,
                Word = TestHelper.CreateStringWithSpecificLength(length),
                Description = TestHelper.CreateStringWithSpecificLength(MAXDESCRIPTIONLENGTH)
            };
            var request = new UpdateDictionaryItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.dictionaryItem.Word);
        }

        [Theory]
        [InlineData(501)]
        [InlineData(MAXDESCRIPTIONLENGTH + 10)]
        public void UpdateDictionaryItemCommand_DescriptionLengthIsGreaterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new DictionaryItemDto()
            {
                Id = 1,
                Word = TestHelper.CreateStringWithSpecificLength(MAXNAMELENGTH),
                Description = TestHelper.CreateStringWithSpecificLength(length)
            };
            var request = new UpdateDictionaryItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.dictionaryItem.Description);
        }

        [Fact]
        public void CreateDictionaryItemCommand_ValidData_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new DictionaryItemDto()
            {
                Id = 1,
                Word = TestHelper.CreateStringWithSpecificLength(MAXNAMELENGTH),
                Description = TestHelper.CreateStringWithSpecificLength(MAXDESCRIPTIONLENGTH)
            };
            var request = new UpdateDictionaryItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.dictionaryItem.Word);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.dictionaryItem.Description);
        }
    }
}
