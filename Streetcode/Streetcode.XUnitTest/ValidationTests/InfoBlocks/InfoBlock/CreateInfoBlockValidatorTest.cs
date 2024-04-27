using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.InfoBlocks;
using Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Create;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.InfoBlocks.InfoBlock
{
    public class CreateInfoBlockValidatorTest
    {
        private readonly CreateInfoBlockCommandValidator _validator;

        public CreateInfoBlockValidatorTest()
        {
            _validator = new CreateInfoBlockCommandValidator();
        }

        [Fact]
        public void ValidVideoUrl_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var dto = new InfoBlockCreateDto
            {
                VideoURL = "https://www.youtube.com/watch?v=abc123"
            };

            var request = new CreateInfoBlockCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(c => c.NewInfoBlock.VideoURL);
        }

        [Theory]
        [InlineData("https://www.youtube.com/watch?v=abc123")]
        [InlineData("https://www.youtube.com/embed/abc123")]
        public void ValidYouTubeUrls_ShouldNotHaveValidationErrors(string url)
        {
            // Arrange
            var dto = new InfoBlockCreateDto
            {
                VideoURL = url
            };

            var request = new CreateInfoBlockCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(c => c.NewInfoBlock.VideoURL);
        }

        [Theory]
        [InlineData("https://vimeo.com/123456")]
        [InlineData("https://example.com/video")]
        [InlineData("invalid-url")]
        public void InvalidUrls_ShouldHaveValidationErrors(string url)
        {
            // Arrange
            var dto = new InfoBlockCreateDto
            {
                VideoURL = url
            };

            var request = new CreateInfoBlockCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(c => c.NewInfoBlock.VideoURL);
        }
    }
}
