// Necessary usings.
using FluentValidation;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Create
{
    /// <summary>
    /// Validator, that validates a model inside CreateStreetcodeCategoryContentCommand.
    /// </summary>
    public sealed class CreateStreetcodeCategoryContentCommandValidator : AbstractValidator<CreateStreetcodeCategoryContentCommand>
    {
        // Constructor
        public CreateStreetcodeCategoryContentCommandValidator()
        {
            // Text max length
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
