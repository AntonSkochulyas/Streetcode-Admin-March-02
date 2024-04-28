using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.Team;
using Streetcode.BLL.MediatR.Team.Create;
using Streetcode.BLL.MediatR.Team.Position.Create;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.Team.Position
{
    public class PositionCommandValidatorTest
    {
        private readonly CreatePositionCommandValidator _validator;
        public PositionCommandValidatorTest()
        {
            _validator = new CreatePositionCommandValidator();
        }

        [Theory]
        [InlineData(44)]
        [InlineData(50)]
        [InlineData(1)]
        [InlineData(3)]
        public void Position_Max_Length_Less_Than_50_Should_Pass(int positionLength)
        {
            string position = string.Empty;

            for (int i = 0; i < positionLength; i++)
            {
                position += "A";
            }

            // Arrange
            var dto = new PositionDto() { Position = position };
            var request = new CreatePositionQuery(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Position.Position);
        }

        [Theory]
        [InlineData(101)]
        [InlineData(105)]
        [InlineData(1002)]
        [InlineData(444)]
        public void Position_Max_Length_Bigger_Than_50_Should_Not_Pass(int positionLength)
        {
            string position = string.Empty;

            for (int i = 0; i < positionLength; i++)
            {
                position += "A";
            }

            // Arrange
            var dto = new PositionDto() { Position = position };
            var request = new CreatePositionQuery(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Position.Position);
        }

        public void Position_Is_Required_Should_Pass()
        {
            string position = "test";

            // Arrange
            var dto = new PositionDto() { Position = position };
            var request = new CreatePositionQuery(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Position.Position);
        }

        public void Position_Is_Required_Should_Not_Pass()
        {
            string position = string.Empty;

            // Arrange
            var dto = new PositionDto() { Position = position };
            var request = new CreatePositionQuery(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Position.Position);
        }
    }
}
