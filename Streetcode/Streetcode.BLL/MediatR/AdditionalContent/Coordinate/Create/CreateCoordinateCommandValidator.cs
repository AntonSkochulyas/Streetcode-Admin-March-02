using FluentValidation;

namespace Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create
{
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
