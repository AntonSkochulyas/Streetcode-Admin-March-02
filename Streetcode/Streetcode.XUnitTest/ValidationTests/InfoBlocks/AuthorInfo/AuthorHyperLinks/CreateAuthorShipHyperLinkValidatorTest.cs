using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.InfoBlocks.Articles;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.BLL.MediatR.InfoBlocks.Articles.Create;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Create;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.InfoBlocks.AuthorInfo.AuthorHyperLinks
{
    public class CreateAuthorShipHyperLinkValidatorTest
    {
        private const int MAXTITLELENGTH = 150;

        private readonly CreateAuthorShipHyperLinkCommandValidator _validator;

        public CreateAuthorShipHyperLinkValidatorTest()
        {
            _validator = new CreateAuthorShipHyperLinkCommandValidator();
        }

        [Theory]
        [InlineData(151)]
        [InlineData(MAXTITLELENGTH + 100)]
        public void CreateAuthorShipHyperLinkCommand_TitleIsGreaterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new AuthorShipHyperLinkCreateDto()
            {
                Title = CreateStringWithSpecificLength(length),
            };
            var request = new CreateAuthorShipHyperLinkCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.NewAuthorHyperLink.Title);
        }

        [Fact]
        public void CreateAuthorShipHyperLinkArticleCommand_ValidData_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new AuthorShipHyperLinkCreateDto()
            {
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH)
            };
            var request = new CreateAuthorShipHyperLinkCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.NewAuthorHyperLink.Title);
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
