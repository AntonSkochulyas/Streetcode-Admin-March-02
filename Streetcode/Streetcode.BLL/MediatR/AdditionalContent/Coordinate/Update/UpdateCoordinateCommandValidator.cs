using FluentValidation;

namespace Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Update
{
    internal class UpdateCoordinateCommandValidator : AbstractValidator<UpdateCoordinateCommand>
    {
        public UpdateCoordinateCommandValidator()
        {
            RuleFor(command => command.StreetcodeCoordinate.Latitude).NotEmpty().WithMessage(CoordinateErrors.UpdateCoordinateHandlerLatitudeIsRequiredError);
            RuleFor(command => command.StreetcodeCoordinate.Longtitude).NotEmpty().WithMessage(CoordinateErrors.UpdateCoordinateHandlerLongtitudeIsRequiredError);
        }
    }
}
