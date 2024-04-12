// Necesassry usings
using FluentValidation;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Partners.Update
{
    /// <summary>
    /// Validator, that validates a model inside a UpdatePartnerQuery.
    /// </summary>
    internal class UpdatePartnerCommandValidator : AbstractValidator<UpdatePartnerQuery>
    {
        // Constructor
        public UpdatePartnerCommandValidator()
        {
            // Title max length
            int titleMaxLength = 100;

            // Target URL max length
            int targetURLMaxLength = 200;

            // URL title max length
            int urlTitleMaxLength = 255;

            // Description max length
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
