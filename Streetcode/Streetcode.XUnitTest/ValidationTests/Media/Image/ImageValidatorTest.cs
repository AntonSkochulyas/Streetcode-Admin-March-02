using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.Media.Audio;
using Streetcode.BLL.Dto.Media.Images;
using Streetcode.BLL.MediatR.Media.Audio.Create;
using Streetcode.BLL.MediatR.Media.Image.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.Media.Image
{
    public class ImageValidatorTest
    {
        private readonly CreateImageCommandValidator _validator;

        public ImageValidatorTest()
        {
            _validator = new CreateImageCommandValidator();
        }

        [Fact]
        public void Title_Is_Required_Should_Not_Pass()
        {
            // Arrange
            var dto = new ImageFileBaseCreateDto() { };
            var request = new CreateImageCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Image.Title);
        }

        [Fact]
        public void Title_Is_Required_Should_Pass()
        {
            // Arrange
            var dto = new ImageFileBaseCreateDto() { Title = "A" };
            var request = new CreateImageCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Image.Title);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(9)]
        public void Title_Max_Length_Should_Pass(int titleLength)
        {
            // Arrange
            string title = string.Empty;

            for (int i = 0; i < titleLength; i++)
            {
                title += "A";
            }

            var dto = new ImageFileBaseCreateDto() { Title = title };
            var request = new CreateImageCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Image.Title);
        }

        [Theory]
        [InlineData(254)]
        [InlineData(101)]
        [InlineData(333)]
        public void Title_Max_Length_Should_Not_Pass(int titleLength)
        {
            // Arrange
            string title = string.Empty;

            for (int i = 0; i < titleLength; i++)
            {
                title += "A";
            }

            var dto = new ImageFileBaseCreateDto() { Title = title };
            var request = new CreateImageCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Image.Title);
        }

        [Fact]
        public void MimeType_Is_Required_Should_Not_Pass()
        {
            // Arrange
            var dto = new ImageFileBaseCreateDto() { };
            var request = new CreateImageCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Image.MimeType);
        }

        [Fact]
        public void MimeType_Is_Required_Should_Pass()
        {
            // Arrange
            var dto = new ImageFileBaseCreateDto() { MimeType = "A" };
            var request = new CreateImageCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Image.MimeType);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(9)]
        public void MimeType_Max_Length_Should_Pass(int mimeTypeLength)
        {
            // Arrange
            string mimeType = string.Empty;

            for (int i = 0; i < mimeTypeLength; i++)
            {
                mimeType += "A";
            }

            var dto = new ImageFileBaseCreateDto() { MimeType = mimeType };
            var request = new CreateImageCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Image.MimeType);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(101)]
        [InlineData(333)]
        public void MimeType_Max_Length_Should_Not_Pass(int mimeTypeLength)
        {
            // Arrange
            string mimeType = string.Empty;

            for (int i = 0; i < mimeTypeLength; i++)
            {
                mimeType += "A";
            }

            var dto = new ImageFileBaseCreateDto() { MimeType = mimeType };
            var request = new CreateImageCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Image.MimeType);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(9)]
        public void Alt_Max_Length_Should_Pass(int altLength)
        {
            // Arrange
            string alt = string.Empty;

            for (int i = 0; i < altLength; i++)
            {
                alt += "A";
            }

            var dto = new ImageFileBaseCreateDto() { Alt = alt };
            var request = new CreateImageCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Image.Alt);
        }

        [Theory]
        [InlineData(2221)]
        [InlineData(101)]
        [InlineData(333)]
        public void Alt_Max_Length_Should_Not_Pass(int altLength)
        {
            // Arrange
            string alt = string.Empty;

            for (int i = 0; i < altLength; i++)
            {
                alt += "A";
            }

            var dto = new ImageFileBaseCreateDto() { Alt = alt };
            var request = new CreateImageCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Image.Alt);
        }
    }
}
