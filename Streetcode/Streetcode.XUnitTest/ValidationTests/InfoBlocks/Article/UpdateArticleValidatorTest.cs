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
using Streetcode.XUnitTest.ValidationTests.TestHelperMethods;
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
                Title = TestHelper.CreateStringWithSpecificLength(length),
                Text = TestHelper.CreateStringWithSpecificLength(MAXTEXTLENGTH),
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
                Title = TestHelper.CreateStringWithSpecificLength(MAXTITLELENGTH),
                Text = TestHelper.CreateStringWithSpecificLength(length),
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
                Title = TestHelper.CreateStringWithSpecificLength(MAXTITLELENGTH),
                Text = TestHelper.CreateStringWithSpecificLength(MAXTEXTLENGTH),
            };
            var request = new UpdateArticleCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Article.Text);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Article.Title);
        }
    }
}
