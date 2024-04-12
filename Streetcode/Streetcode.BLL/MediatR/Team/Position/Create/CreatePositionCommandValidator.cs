using FluentValidation;
using Streetcode.BLL.MediatR.Team.Create;

namespace Streetcode.BLL.MediatR.Team.Position.Create
{
    internal class CreatePositionCommandValidator : AbstractValidator<CreatePositionQuery>
    {
        public CreatePositionCommandValidator()
        {
            int positionMaxLength = 50;

            RuleFor(command => command.Position.Position)
                .NotEmpty()
                .WithMessage(TeamErrors.CreatePositionCommandValidatorPositionIsRequiredError)
                .MaximumLength(positionMaxLength)
                .WithMessage(string.Format(TeamErrors.CreatePositionCommandValidatorPositionMaxLengthError, positionMaxLength));
        }
    }
}
