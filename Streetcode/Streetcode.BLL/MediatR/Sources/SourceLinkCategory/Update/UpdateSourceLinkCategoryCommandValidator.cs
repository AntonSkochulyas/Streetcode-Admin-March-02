using FluentValidation;

namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Update
{
    public sealed class UpdateSourceLinkCategoryCommandValidator : AbstractValidator<UpdateSourceLinkCategoryCommand>
    {
        public UpdateSourceLinkCategoryCommandValidator()
        {
            ushort maxTitleLength = 100;

            RuleFor(command => command.SourceLinkDto.Title)
                .NotEmpty()
                .WithMessage(SourceErrors.CreateSourceLinkCategoryCommandValidatorTitleIsRequiredError)
                .MaximumLength(maxTitleLength)
                .WithMessage(string.Format(SourceErrors.CreateSourceLinkCategoryCommandValidatorTitleMaxLengthError, maxTitleLength));

            RuleFor(command => command.SourceLinkDto.ImageId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage(SourceErrors.CreateSourceLinkCategoryCommandValidatorImageIsRequiredError);
        }
    }
}
