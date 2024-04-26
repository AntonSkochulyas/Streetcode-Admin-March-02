using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.InfoBlocks;
using Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Create;
using Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Update;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.InfoBlocks.InfoBlock
{
    public class UpdateInfoBlockValidatorTest
    {
        private readonly UpdateInfoBlockCommandValidator _validator;

        public UpdateInfoBlockValidatorTest()
        {
            _validator = new UpdateInfoBlockCommandValidator();
        }

        [Fact]
        public void ValidVideoUrl_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var dto = new InfoBlockDto
            {
                Id = 1,
                VideoURL = "https://www.youtube.com/watch?v=abc123"
            };

            var request = new UpdateInfoBlockCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(c => c.InfoBlock.VideoURL);
        }

        [Theory]
        [InlineData("https://www.youtube.com/watch?v=abc123")]
        [InlineData("https://www.youtube.com/embed/abc123")]
        public void ValidYouTubeUrls_ShouldNotHaveValidationErrors(string url)
        {
            // Arrange
            var dto = new InfoBlockDto
            {
                Id = 1,
                VideoURL = url
            };

            var request = new UpdateInfoBlockCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(c => c.InfoBlock.VideoURL);
        }

        [Theory]
        [InlineData("https://vimeo.com/123456")]
        [InlineData("https://example.com/video")]
        [InlineData("invalid-url")]
        public void InvalidUrls_ShouldHaveValidationErrors(string url)
        {
            // Arrange
            var dto = new InfoBlockDto
            {
                Id = 1,
                VideoURL = url
            };

            var request = new UpdateInfoBlockCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(c => c.InfoBlock.VideoURL);
        }
    }
}
