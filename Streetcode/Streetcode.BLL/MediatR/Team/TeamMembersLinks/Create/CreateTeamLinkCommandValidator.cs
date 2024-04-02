using FluentValidation;

namespace Streetcode.BLL.MediatR.Team.TeamMembersLinks.Create
{
    internal class CreateTeamLinkCommandValidator : AbstractValidator<CreateTeamLinkQuery>
    {
        private readonly int _targetUrlMaxLength;

        public CreateTeamLinkCommandValidator()
        {
            _targetUrlMaxLength = 255;

            RuleFor(command => command.teamMember.LogoType)
                .NotEmpty()
                .WithMessage(TeamErrors.CreateTeamLinkCommandValidatorLogoTypeIsRequiredError);

            RuleFor(command => command.teamMember.TargetUrl)
                .NotEmpty()
                .WithMessage(TeamErrors.CreateTeamLinkCommandValidatorTargetUrlIsRequiredError)
                .MaximumLength(_targetUrlMaxLength)
                .WithMessage(string.Format(TeamErrors.CreateTeamLinkCommandValidatorTargetUrlMaxLengthError, _targetUrlMaxLength));

            RuleFor(command => command.teamMember.TeamMemberId)
                .NotEmpty()
                .WithMessage(TeamErrors.CreateTeamLinkCommandValidatorTeamMemberIdIsRequiredError);
        }
    }
}
