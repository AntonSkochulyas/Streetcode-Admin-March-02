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
            RuleFor(command => command.CreateStreetcodeCoordinateDto.Latitude)
                .NotEmpty()
                .WithMessage(CoordinateErrors.CreateCoordinateCommandValidatorLatitudeIsRequiredError);

            RuleFor(command => command.CreateStreetcodeCoordinateDto.Longtitude)
                .NotEmpty()
                .WithMessage(CoordinateErrors.CreateCoordinateCommandValidatorLongtitudeIsRequiredError);
        }
    }
}
