using FluentValidation;

namespace Streetcode.BLL.MediatR.Partners.Create
{
    internal class CreatePartnerCommandValidator : AbstractValidator<CreatePartnerCommand>
    {
        public CreatePartnerCommandValidator()
        {
            int titleMaxLength = 100;
            int targetURLMaxLength = 200;
            int urlTitleMaxLength = 255;
            int descriptionMaxLength = 450;

            RuleFor(command => command.NewPartner.Title)
                .NotEmpty()
                .WithMessage(PartnersErrors.CreatePartnerCommandValidatorTitleIsRequiredError)
                .MaximumLength(titleMaxLength)
                .WithMessage(string.Format(PartnersErrors.CreatePartnerCommandValidatorTitleMaxLengthError, titleMaxLength));

            RuleFor(command => command.NewPartner.LogoId)
                .NotEmpty()
                .WithMessage(PartnersErrors.CreatePartnerCommandValidatorLogoIdIsRequiredError);

            RuleFor(command => command.NewPartner.IsKeyPartner)
              .NotEmpty()
              .WithMessage(PartnersErrors.CreatePartnerCommandValidatorIsKeyPartnerIsRequiredError);

            RuleFor(command => command.NewPartner.IsVisibleEverywhere)
             .NotEmpty()
             .WithMessage(PartnersErrors.CreatePartnerCommandValidatorIsVisibleEverywhereIsRequiredError);

            RuleFor(command => command.NewPartner.TargetUrl)
               .MaximumLength(targetURLMaxLength)
               .WithMessage(string.Format(PartnersErrors.CreatePartnerCommandValidatorTargetURLMaxLengthError, targetURLMaxLength));

            RuleFor(command => command.NewPartner.UrlTitle)
               .MaximumLength(urlTitleMaxLength)
               .WithMessage(string.Format(PartnersErrors.CreatePartnerCommandValidatorURLTitleMaxLengthError, urlTitleMaxLength));

            RuleFor(command => command.NewPartner.Description)
              .MaximumLength(descriptionMaxLength)
              .WithMessage(PartnersErrors.CreatePartnerCommandValidatorDescriptionMaxLengthError);
        }
    }
}
