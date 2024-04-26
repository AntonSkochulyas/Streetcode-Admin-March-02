// Necessary usings.
using FluentValidation;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Create
{
    /// <summary>
    /// Validator, that validates a model inside CreateSourceLinkCommand.
    /// </summary>
    public sealed class CreateSourceLinkCategoryCommandValidator : AbstractValidator<CreateSourceLinkCategoryCommand>
    {
        // Constructor
        public CreateSourceLinkCategoryCommandValidator()
        {
            // Title max length
            ushort maxTitleLength = 100;

            RuleFor(command => command.SourceLinkCategoryContentDto.Title)
                .NotEmpty()
                .WithMessage(SourceErrors.CreateSourceLinkCategoryCommandValidatorTitleIsRequiredError)
                .MaximumLength(maxTitleLength)
                .WithMessage(string.Format(SourceErrors.CreateSourceLinkCategoryCommandValidatorTitleMaxLengthError, maxTitleLength));

            RuleFor(command => command.SourceLinkCategoryContentDto.ImageId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage(SourceErrors.CreateSourceLinkCategoryCommandValidatorImageIsRequiredError);
        }
    }
}