using FluentValidation;

namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Update
{
    public sealed class UpdateSourceLinkCategoryCommandValidator : AbstractValidator<UpdateSourceLinkCategoryCommand>
    {
        public UpdateSourceLinkCategoryCommandValidator()
        {
            ushort maxTitleLength = 100;

            RuleFor(command => command.sourceLinkCategoryDto.Title)
                .NotEmpty()
                .WithMessage(SourceErrors.UpdateSourceLinkCategoryCommandValidatorTitleIsRequiredError)
                .MaximumLength(maxTitleLength)
                .WithMessage(string.Format(SourceErrors.UpdateSourceLinkCategoryCommandValidatorTitleMaxLengthError, maxTitleLength));

            RuleFor(command => command.sourceLinkCategoryDto.Image)
                .NotNull()
                .WithMessage(SourceErrors.UpdateSourceLinkCategoryCommandValidatorCreationImageIsRequiredError);
        }
    }
}
