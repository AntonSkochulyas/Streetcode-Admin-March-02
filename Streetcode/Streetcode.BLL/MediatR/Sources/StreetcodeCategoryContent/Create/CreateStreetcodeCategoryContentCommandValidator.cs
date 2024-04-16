using FluentValidation;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory;

namespace Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Create
{
    public sealed class CreateStreetcodeCategoryContentCommandValidator : AbstractValidator<CreateStreetcodeCategoryContentCommand>
    {
        public CreateStreetcodeCategoryContentCommandValidator()
        {
            ushort maxTextLength = 4000;

            RuleFor(command => command.StreetcodeCategoryContentDto.Text)
                .MaximumLength(4000)
                .WithMessage(string.Format(SourceErrors.CreateStreetcodeCategoryContentCommandTextLengthMaxLengthError, maxTextLength));

            RuleFor(command => command.StreetcodeCategoryContentDto.SourceLinkCategoryId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage(string.Format(SourceErrors.CreateStreetcodeCategoryContentCommandValidatorSourceLinkIdIsRequiredError));

            RuleFor(command => command.StreetcodeCategoryContentDto.StreetcodeId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage(SourceErrors.CreateStreetcodeCategoryContentCommandValidatorStreetcodeIdIsRequiredError);
        }
    }
}
