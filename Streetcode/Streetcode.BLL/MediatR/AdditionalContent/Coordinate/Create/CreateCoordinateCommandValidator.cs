// Necessary usings
using FluentValidation;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create
{
    /// <summary>
    /// Validator, that validate a model inside CreateCoordinateCommand.
    /// </summary>
    public class CreateCoordinateCommandValidator : AbstractValidator<CreateCoordinateCommand>
    {
        public CreateCoordinateCommandValidator()
        {
            RuleFor(command => command.CreateStreetcodeCoordinateDto.Latitude)
                .NotEmpty()
                .WithMessage(CoordinateErrors.CreateCoordinateCommandValidatorLatitudeIsRequiredError)
                .GreaterThanOrEqualTo(-90)
                .WithMessage(CoordinateErrors.CreateCoordinateCommandValidatorLatitudeRange)
                .LessThanOrEqualTo(90)
                .WithMessage(CoordinateErrors.CreateCoordinateCommandValidatorLatitudeRange);

            RuleFor(command => command.CreateStreetcodeCoordinateDto.Longtitude)
                .NotEmpty()
                .WithMessage(CoordinateErrors.CreateCoordinateCommandValidatorLongtitudeIsRequiredError)
                .GreaterThanOrEqualTo(-180)
                .WithMessage(CoordinateErrors.CreateCoordinateCommandValidatorLongtitudeRange)
                .LessThanOrEqualTo(180)
                .WithMessage(CoordinateErrors.CreateCoordinateCommandValidatorLongtitudeRange);
        }
    }
}
