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
        // Title max length
        private readonly int _titleMaxLength;

        // Target URL max length
        private readonly int _targetURLMaxLength;

        // URL title max length
        private readonly int _urlTitleMaxLength;

        // Description max length
        private readonly int _descriptionMaxLength;

        // Constructor
        public UpdatePartnerCommandValidator()
        {
            _titleMaxLength = 100;
            _targetURLMaxLength = 200;
            _urlTitleMaxLength = 255;
            _descriptionMaxLength = 450;

            RuleFor(command => command.Partner.Title)
                .NotEmpty()
                .WithMessage(PartnersErrors.UpdatePartnerCommandValidatorTitleIsRequiredError)
                .MaximumLength(_titleMaxLength)
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
               .MaximumLength(_targetURLMaxLength)
               .WithMessage(PartnersErrors.UpdatePartnerCommandValidatorTargetURLMaxLengthError);

            RuleFor(command => command.Partner.UrlTitle)
               .MaximumLength(_urlTitleMaxLength)
               .WithMessage(string.Format(PartnersErrors.UpdatePartnerCommandValidatorURLTitleMaxLengthError, _urlTitleMaxLength));

            RuleFor(command => command.Partner.Description)
              .MaximumLength(_descriptionMaxLength)
              .WithMessage(string.Format(PartnersErrors.UpdatePartnerCommandValidatorDescriptionMaxLengthError, _descriptionMaxLength));
        }
    }
}
