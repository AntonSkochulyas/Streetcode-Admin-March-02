// Necessary usings.
using FluentValidation;

// Necessaru namespaces.
namespace Streetcode.BLL.MediatR.Partners.Create
{
    /// <summary>
    /// Validator, that validates a model inside CreatePartnerCommand.
    /// </summary>
    internal class CreatePartnerCommandValidator : AbstractValidator<CreatePartnerCommand>
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
        public CreatePartnerCommandValidator()
        {
            _titleMaxLength = 100;
            _targetURLMaxLength = 200;
            _urlTitleMaxLength = 255;
            _descriptionMaxLength = 450;

            RuleFor(command => command.newPartner.Title)
                .NotEmpty()
                .WithMessage(PartnersErrors.CreatePartnerCommandValidatorTitleIsRequiredError)
                .MaximumLength(_titleMaxLength)
                .WithMessage(string.Format(PartnersErrors.CreatePartnerCommandValidatorTitleMaxLengthError, _titleMaxLength));

            RuleFor(command => command.newPartner.LogoId)
                .NotEmpty()
                .WithMessage(PartnersErrors.CreatePartnerCommandValidatorLogoIdIsRequiredError);

            RuleFor(command => command.newPartner.IsKeyPartner)
              .NotEmpty()
              .WithMessage(PartnersErrors.CreatePartnerCommandValidatorIsKeyPartnerIsRequiredError);

            RuleFor(command => command.newPartner.IsVisibleEverywhere)
             .NotEmpty()
             .WithMessage(PartnersErrors.CreatePartnerCommandValidatorIsVisibleEverywhereIsRequiredError);

            RuleFor(command => command.newPartner.TargetUrl)
               .MaximumLength(_targetURLMaxLength)
               .WithMessage(string.Format(PartnersErrors.CreatePartnerCommandValidatorTargetURLMaxLengthError, _targetURLMaxLength));

            RuleFor(command => command.newPartner.UrlTitle)
               .MaximumLength(_urlTitleMaxLength)
               .WithMessage(string.Format(PartnersErrors.CreatePartnerCommandValidatorURLTitleMaxLengthError, _urlTitleMaxLength));

            RuleFor(command => command.newPartner.Description)
              .MaximumLength(_descriptionMaxLength)
              .WithMessage(PartnersErrors.CreatePartnerCommandValidatorDescriptionMaxLengthError);
        }
    }
}
