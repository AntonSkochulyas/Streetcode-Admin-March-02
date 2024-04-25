using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.BLL.Dto.InfoBlocks.Articles;
using Streetcode.BLL.MediatR.Dictionaries.Create;
using Streetcode.BLL.MediatR.InfoBlocks.Articles.Create;
using Streetcode.BLL.MediatR.InfoBlocks.Articles.Update;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.InfoBlocks.Article
{
    public class UpdateArticleValidatorTest
    {
        private const int MAXTITLELENGTH = 50;
        private const int MAXTEXTLENGTH = 15000;

        private readonly UpdateArticleCommandValidator _validator;

        public UpdateArticleValidatorTest()
        {
            _validator = new UpdateArticleCommandValidator();
        }

        [Theory]
        [InlineData(60)]
        [InlineData(MAXTITLELENGTH + 100)]
        public void UpdateArticleCommand_TitleIsGreaterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new ArticleDto()
            {
                Id = 1,
                Title = CreateStringWithSpecificLength(length),
                Text = CreateStringWithSpecificLength(MAXTEXTLENGTH),
            };
            var request = new UpdateArticleCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Article.Title);
        }

        [Theory]
        [InlineData(15001)]
        [InlineData(MAXTEXTLENGTH + 100)]
        public void UpdateArticleCommand_TextIsGreaterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new ArticleDto()
            {
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH),
                Text = CreateStringWithSpecificLength(length),
            };
            var request = new UpdateArticleCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Article.Text);
        }

        [Fact]
        public void UpdateArticleCommand_ValidData_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new ArticleDto()
            {
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH),
                Text = CreateStringWithSpecificLength(MAXTEXTLENGTH),
            };
            var request = new UpdateArticleCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Article.Text);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Article.Title);
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
