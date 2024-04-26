using FluentValidation;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Update;

namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Update
{
    public sealed class UpdateStreetcodeCategoryContentCommandValidator : AbstractValidator<UpdateStreetcodeCategoryContentCommand>
    {
        public UpdateStreetcodeCategoryContentCommandValidator()
        {
            ushort maxTextLength = 4000;

            RuleFor(command => command.StreetcodeCategoryContentDto.Text)
                .MaximumLength(maxTextLength)
                .WithMessage(string.Format(SourceErrors.UpdateStreetcodeCategoryContentCommandValidatorTextMaxLengthError, maxTextLength));

            RuleFor(command => command.StreetcodeCategoryContentDto.SourceLinkCategoryId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage(string.Format(SourceErrors.UpdateStreetcodeCategoryContentCommandValidatorSourceLinkIdIsRequiredError));

            RuleFor(command => command.StreetcodeCategoryContentDto.StreetcodeId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage(SourceErrors.UpdateStreetcodeCategoryContentCommandValidatorStreetcodeIdIsRequired);
        }
    }
}
