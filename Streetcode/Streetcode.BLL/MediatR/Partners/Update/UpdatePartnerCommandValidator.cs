using FluentValidation;

namespace Streetcode.BLL.MediatR.Partners.Update
{
    internal class UpdatePartnerCommandValidator : AbstractValidator<UpdatePartnerQuery>
    {
        private readonly int _titleMaxLength;
        private readonly int _targetURLMaxLength;
        private readonly int _urlTitleMaxLength;
        private readonly int _descriptionMaxLength;

        public UpdatePartnerCommandValidator()
        {
            _titleMaxLength = 255;
            _targetURLMaxLength = 255;
            _urlTitleMaxLength = 255;
            _descriptionMaxLength = 600;

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
