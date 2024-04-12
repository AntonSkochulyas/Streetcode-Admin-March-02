using FluentValidation;

namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Create
{
    public sealed class CreateSourceLinkCategoryCommandValidator : AbstractValidator<CreateSourceLinkCategoryCommand>
    {
        public CreateSourceLinkCategoryCommandValidator()
        {
            ushort maxTitleLength = 100;
            ushort maxTextLength = 4000;

            RuleFor(command => command.SourceLinkCategoryContentDto.Title)
                .NotEmpty()
                .WithMessage(SourceErrors.CreateSourceLinkCategoryCommandValidatorTitleIsRequiredError)
                .MaximumLength(maxTitleLength)
                .WithMessage(string.Format(SourceErrors.CreateSourceLinkCategoryCommandValidatorTitleMaxLengthError, maxTitleLength));

            RuleFor(command => command.SourceLinkCategoryContentDto.Text)
                .MaximumLength(maxTextLength)
                .WithMessage(string.Format(SourceErrors.CreateStreetcodeCategoryContentCommandTextLengthMaxLengthError, maxTitleLength));

            RuleFor(command => command.SourceLinkCategoryContentDto.ImageId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage(SourceErrors.CreateSourceLinkCategoryCommandValidatorImageIsRequiredError);

            RuleFor(command => command.SourceLinkCategoryContentDto.StreetcodeId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage(SourceErrors.CreateSourceLinkCategoryCommandValidatorStreetcodeIdeIsRequiredError);
        }
    }
}
