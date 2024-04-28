using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.Media.Audio;
using Streetcode.BLL.Dto.Timeline;
using Streetcode.BLL.MediatR.Media.Audio.Create;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.Media.Audio
{
    public class AudioValidatorTest
    {
        private readonly CreateAudioCommandValidator _validator;

        public AudioValidatorTest()
        {
            _validator = new CreateAudioCommandValidator();
        }

        [Fact]
        public void Title_Is_Required_Should_Not_Pass()
        {
            // Arrange
            var dto = new AudioFileBaseCreateDto() { };
            var request = new CreateAudioCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Audio.Title);
        }

        [Fact]
        public void Title_Is_Required_Should_Pass()
        {
            // Arrange
            var dto = new AudioFileBaseCreateDto() { Title = "A" };
            var request = new CreateAudioCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Audio.Title);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        [InlineData(49)]
        public void Title_Max_Length_Should_Pass(int titleLength)
        {
            // Arrange
            string title = string.Empty;

            for (int i = 0; i < titleLength; i++)
            {
                title += "A";
            }

            var dto = new AudioFileBaseCreateDto() { Title = title };
            var request = new CreateAudioCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Audio.Title);
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

            var dto = new AudioFileBaseCreateDto() { Title = title };
            var request = new CreateAudioCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Audio.Title);
        }

        [Fact]
        public void MimeType_Is_Required_Should_Not_Pass()
        {
            // Arrange
            var dto = new AudioFileBaseCreateDto() { };
            var request = new CreateAudioCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Audio.MimeType);
        }

        [Fact]
        public void MimeType_Is_Required_Should_Pass()
        {
            // Arrange
            var dto = new AudioFileBaseCreateDto() { MimeType = "A" };
            var request = new CreateAudioCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Audio.MimeType);
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

            var dto = new AudioFileBaseCreateDto() { MimeType = mimeType };
            var request = new CreateAudioCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Audio.MimeType);
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

            var dto = new AudioFileBaseCreateDto() { MimeType = mimeType };
            var request = new CreateAudioCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Audio.MimeType);
        }
    }
}
