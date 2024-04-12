// Necessary usings
using FluentValidation;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create
{
    /// <summary>
    /// Validator, that validate a model inside CreateCoordinateCommand.
    /// </summary>
    internal class CreateCoordinateCommandValidator : AbstractValidator<CreateCoordinateCommand>
    {
        public CreateCoordinateCommandValidator()
        {
            RuleFor(command => command.StreetcodeCoordinate.Latitude)
                .NotEmpty()
                .WithMessage(CoordinateErrors.CreateCoordinateCommandValidatorLatitudeIsRequiredError);

            RuleFor(command => command.StreetcodeCoordinate.Longtitude)
                .NotEmpty()
                .WithMessage(CoordinateErrors.CreateCoordinateCommandValidatorLongtitudeIsRequiredError);
        }
    }
}
