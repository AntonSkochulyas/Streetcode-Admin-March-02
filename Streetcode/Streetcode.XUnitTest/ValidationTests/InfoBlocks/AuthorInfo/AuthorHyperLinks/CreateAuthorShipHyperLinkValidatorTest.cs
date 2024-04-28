using System.Text;
using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Create;
using Streetcode.XUnitTest.ValidationTests.TestHelperMethods;
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
                Title = TestHelper.CreateStringWithSpecificLength(length),
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
                Title = TestHelper.CreateStringWithSpecificLength(MAXTITLELENGTH)
            };
            var request = new CreateAuthorShipHyperLinkCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.NewAuthorHyperLink.Title);
        }
    }
}
