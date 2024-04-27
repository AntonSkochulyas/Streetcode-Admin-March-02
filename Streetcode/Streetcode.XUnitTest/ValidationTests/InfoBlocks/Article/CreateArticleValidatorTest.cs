using System.Text;
using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.BLL.Dto.InfoBlocks.Articles;
using Streetcode.BLL.MediatR.Dictionaries.Create;
using Streetcode.BLL.MediatR.InfoBlocks.Articles.Create;
using Streetcode.XUnitTest.ValidationTests.TestHelperMethods;
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
                Title = TestHelper.CreateStringWithSpecificLength(length),
                Text = TestHelper.CreateStringWithSpecificLength(MAXTEXTLENGTH),
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
                Title = TestHelper.CreateStringWithSpecificLength(MAXTITLELENGTH),
                Text = TestHelper.CreateStringWithSpecificLength(length),
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
                Title = TestHelper.CreateStringWithSpecificLength(MAXTITLELENGTH),
                Text = TestHelper.CreateStringWithSpecificLength(MAXTEXTLENGTH),
            };
            var request = new CreateArticleCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.NewArticle.Title);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.NewArticle.Text);
        }
    }
}
