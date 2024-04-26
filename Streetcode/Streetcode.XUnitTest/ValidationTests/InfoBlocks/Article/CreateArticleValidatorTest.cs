using System.Text;
using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.BLL.Dto.InfoBlocks.Articles;
using Streetcode.BLL.MediatR.Dictionaries.Create;
using Streetcode.BLL.MediatR.InfoBlocks.Articles.Create;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.InfoBlocks.Article
{
    public class CreateArticleValidatorTest
    {
        private const int MAXTITLELENGTH = 50;
        private const int MAXTEXTLENGTH = 15000;

        private readonly CreateArticleCommandValidator _validator;

        public CreateArticleValidatorTest()
        {
            _validator = new CreateArticleCommandValidator();
        }

        [Theory]
        [InlineData(60)]
        [InlineData(MAXTITLELENGTH + 100)]
        public void CreateArticleCommand_TitleIsGreaterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new ArticleCreateDto()
            {
                Title = CreateStringWithSpecificLength(length),
                Text = CreateStringWithSpecificLength(MAXTEXTLENGTH),
            };
            var request = new CreateArticleCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.NewArticle.Title);
        }

        [Theory]
        [InlineData(15001)]
        [InlineData(MAXTEXTLENGTH + 100)]
        public void CreateArticleCommand_TextIsGreaterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new ArticleCreateDto()
            {
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH),
                Text = CreateStringWithSpecificLength(length),
            };
            var request = new CreateArticleCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.NewArticle.Text);
        }

        [Fact]
        public void CreateArticleCommand_Valid_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new ArticleCreateDto()
            {
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH),
                Text = CreateStringWithSpecificLength(MAXTEXTLENGTH),
            };
            var request = new CreateArticleCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.NewArticle.Title);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.NewArticle.Text);
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
