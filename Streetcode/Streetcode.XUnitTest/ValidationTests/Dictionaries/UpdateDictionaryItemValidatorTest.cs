using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.BLL.MediatR.Dictionaries.Create;
using Streetcode.BLL.MediatR.Dictionaries.Update;
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
                Word = CreateStringWithSpecificLength(length),
                Description = CreateStringWithSpecificLength(MAXDESCRIPTIONLENGTH)
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
                Word = CreateStringWithSpecificLength(MAXNAMELENGTH),
                Description = CreateStringWithSpecificLength(length)
            };
            var request = new UpdateDictionaryItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.dictionaryItem.Description);
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
