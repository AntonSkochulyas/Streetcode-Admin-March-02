using FluentValidation;
using Streetcode.BLL.MediatR.Timeline.HistoricalContext.Create;

namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Create
{
    public sealed class CreateSourceLinkCategoryCommandValidator : AbstractValidator<CreateSourceLinkCategoryCommand>
    {
        public CreateSourceLinkCategoryCommandValidator()
        {
            ushort maxTitleLength = 100;

            RuleFor(command => command.sourceLinkCategoryDto.Title)
                .NotEmpty()
                .WithMessage(SourceErrors.CreateSourceLinkCategoryCommandValidatorTitleIsRequiredError)
                .MaximumLength(maxTitleLength)
                .WithMessage(string.Format(SourceErrors.CreateSourceLinkCategoryCommandValidatorTitleMaxLengthError, maxTitleLength));

            RuleFor(command => command.sourceLinkCategoryDto.Image)
                .NotNull()
                .WithMessage(SourceErrors.CreateSourceLinkCategoryCommandValidatorImageIsRequiredError);
        }
    }
}
