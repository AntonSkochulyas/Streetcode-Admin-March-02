using FluentValidation;
using Streetcode.BLL.MediatR.Team.Create;

namespace Streetcode.BLL.MediatR.Team.Position.Create
{
    internal class CreatePositionCommandValidator : AbstractValidator<CreatePositionQuery>
    {
        private readonly int _positionMaxLength;

        public CreatePositionCommandValidator()
        {
            _positionMaxLength = 50;

            RuleFor(command => command.position.Position)
                .NotEmpty()
                .WithMessage(TeamErrors.CreatePositionCommandValidatorPositionIsRequiredError)
                .MaximumLength(_positionMaxLength)
                .WithMessage(string.Format(TeamErrors.CreatePositionCommandValidatorPositionMaxLengthError, _positionMaxLength));
        }
    }
}
