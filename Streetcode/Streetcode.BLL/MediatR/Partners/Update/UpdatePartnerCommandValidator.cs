using FluentValidation;

namespace Streetcode.BLL.MediatR.Partners.Update
{
    internal class UpdatePartnerCommandValidator : AbstractValidator<UpdatePartnerQuery>
    {
        public UpdatePartnerCommandValidator()
        {
            int titleMaxLength = 100;
            int targetURLMaxLength = 200;
            int urlTitleMaxLength = 255;
            int descriptionMaxLength = 450;

            RuleFor(command => command.Partner.Title)
                .NotEmpty()
                .WithMessage(PartnersErrors.UpdatePartnerCommandValidatorTitleIsRequiredError)
                .MaximumLength(titleMaxLength)
                .WithMessage(PartnersErrors.UpdatePartnerCommandValidatorTitleMaxLengthError);

            RuleFor(command => command.Partner.LogoId)
                .NotEmpty()
                .WithMessage(PartnersErrors.UpdatePartnerCommandValidatorLogoIdIsRequiredError);

            RuleFor(command => command.Partner.IsKeyPartner)
              .NotEmpty()
              .WithMessage(PartnersErrors.UpdatePartnerCommandValidatorIsKeyPartnerIsRequired);

            RuleFor(command => command.Partner.IsVisibleEverywhere)
             .NotEmpty()
             .WithMessage(PartnersErrors.UpdatePartnerCommandValidatorIsVisibleEverywhereIsRequiredError);

            RuleFor(command => command.Partner.TargetUrl)
               .MaximumLength(targetURLMaxLength)
               .WithMessage(PartnersErrors.UpdatePartnerCommandValidatorTargetURLMaxLengthError);

            RuleFor(command => command.Partner.UrlTitle)
               .MaximumLength(urlTitleMaxLength)
               .WithMessage(string.Format(PartnersErrors.UpdatePartnerCommandValidatorURLTitleMaxLengthError, urlTitleMaxLength));

            RuleFor(command => command.Partner.Description)
              .MaximumLength(descriptionMaxLength)
              .WithMessage(string.Format(PartnersErrors.UpdatePartnerCommandValidatorDescriptionMaxLengthError, descriptionMaxLength));
        }
    }
}
