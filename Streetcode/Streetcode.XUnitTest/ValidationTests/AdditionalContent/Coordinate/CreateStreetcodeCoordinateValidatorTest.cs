using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.AdditionalContent.Coordinates;
using Streetcode.BLL.Dto.AdditionalContent.Coordinates.Types;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Create;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.AdditionalContent.Coordinate
{
    public class CreateStreetcodeCoordinateValidatorTest
    {
        private const int MINSTREETCODEID = 1;
        private const int MINLATITUDE = -90;
        private const int MAXLATITUDE = 90;
        private const int MINLONGTITUDE = -180;
        private const int MAXLONGTITUDE = 180;

        private readonly CreateCoordinateCommandValidator _validator;

        public CreateStreetcodeCoordinateValidatorTest()
        {
            _validator = new CreateCoordinateCommandValidator();
        }

        [Theory]
        [InlineData(-91)]
        [InlineData(91)]
        [InlineData(MAXLATITUDE + 100)]
        public void CreateCoordinateCommand_LatitudeIsLessOrGraterThanAllowed_ShouldHaveErrors(int latitude)
        {
            // Arrange
            var dto = new CreateStreetcodeCoordinateDto()
            {
                Latitude = latitude,
                Longtitude = MAXLONGTITUDE,
                StreetcodeId = MINSTREETCODEID
            };
            var request = new CreateCoordinateCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.CreateStreetcodeCoordinateDto.Latitude);
        }

        [Theory]
        [InlineData(-181)]
        [InlineData(181)]
        [InlineData(MAXLONGTITUDE + 100)]
        public void CreateCoordinateCommand_LongtitudeIsLessOrGraterThanAllowed_ShouldHaveErrors(int longtitude)
        {
            // Arrange
            var dto = new CreateStreetcodeCoordinateDto()
            {
                Latitude = MAXLATITUDE,
                Longtitude = longtitude,
                StreetcodeId = MINSTREETCODEID
            };
            var request = new CreateCoordinateCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.CreateStreetcodeCoordinateDto.Longtitude);
        }

        [Fact]
        public void CreateCoordinateCommand_ValidData_ShouldNotHaveErrors()
        {
            var dto = new CreateStreetcodeCoordinateDto()
            {
                Latitude = MINLATITUDE,
                Longtitude = MINLONGTITUDE,
                StreetcodeId = MINSTREETCODEID
            };
            var request = new CreateCoordinateCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.CreateStreetcodeCoordinateDto.StreetcodeId);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.CreateStreetcodeCoordinateDto.Latitude);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.CreateStreetcodeCoordinateDto.Longtitude);
        }
    }
}
