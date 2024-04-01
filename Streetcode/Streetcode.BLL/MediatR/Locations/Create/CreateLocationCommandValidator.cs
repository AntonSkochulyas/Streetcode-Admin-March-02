using FluentValidation;

namespace Streetcode.BLL.MediatR.Locations.Create
{
    public sealed class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
    {
        private readonly ushort _maxStreetnameLength;

        public CreateLocationCommandValidator()
        {
            _maxStreetnameLength = 128;

            RuleFor(command => command.newLocation.Streetname)
                .NotEmpty()
                .MaximumLength(_maxStreetnameLength)
                .WithMessage(string.Format(LocationsErrors.CreateLocationCommandValidatorStreetNameError, _maxStreetnameLength));

            RuleFor(command => command.newLocation.TableNumber)
                .NotEmpty();
        }
    }
}
