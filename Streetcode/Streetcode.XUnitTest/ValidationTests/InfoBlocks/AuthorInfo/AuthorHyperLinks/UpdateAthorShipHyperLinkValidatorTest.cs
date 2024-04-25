using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Create;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Update;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.InfoBlocks.AuthorInfo.AuthorHyperLinks
{
    public class UpdateAthorShipHyperLinkValidatorTest
    {
        private const int MAXTITLELENGTH = 150;

        private readonly UpdateAuthorShipHyperLinkCommandValidator _validator;

        public UpdateAthorShipHyperLinkValidatorTest()
        {
            _validator = new UpdateAuthorShipHyperLinkCommandValidator();
        }

        [Theory]
        [InlineData(151)]
        [InlineData(MAXTITLELENGTH + 100)]
        public void UpdateAuthorShipHyperLinkCommand_TitleIsGreaterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new AuthorShipHyperLinkDto()
            {
                Id = 1,
                Title = CreateStringWithSpecificLength(length),
            };
            var request = new UpdateAuthorShipHyperLinkCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.AuthorsHyperLink.Title);
        }

        [Fact]
        public void UpdateAuthorShipHyperLinkArticleCommand_ValidData_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new AuthorShipHyperLinkDto()
            {
                Id = 1,
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH)
            };
            var request = new UpdateAuthorShipHyperLinkCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.AuthorsHyperLink.Title);
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
