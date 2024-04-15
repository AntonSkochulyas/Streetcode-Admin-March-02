using FluentValidation;

namespace Streetcode.BLL.MediatR.Team.TeamMembersLinks.Create
{
    internal class CreateTeamLinkCommandValidator : AbstractValidator<CreateTeamLinkQuery>
    {
        public CreateTeamLinkCommandValidator()
        {
            int targetUrlMaxLength = 255;

            RuleFor(command => command.TeamMember.LogoType)
                .NotEmpty()
                .WithMessage(TeamErrors.CreateTeamLinkCommandValidatorLogoTypeIsRequiredError);

            RuleFor(command => command.TeamMember.TargetUrl)
                .NotEmpty()
                .WithMessage(TeamErrors.CreateTeamLinkCommandValidatorTargetUrlIsRequiredError)
                .MaximumLength(targetUrlMaxLength)
                .WithMessage(string.Format(TeamErrors.CreateTeamLinkCommandValidatorTargetUrlMaxLengthError, targetUrlMaxLength));

            RuleFor(command => command.TeamMember.TeamMemberId)
                .NotEmpty()
                .WithMessage(TeamErrors.CreateTeamLinkCommandValidatorTeamMemberIdIsRequiredError);
        }
    }
}
