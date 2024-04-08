using FluentValidation;

namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Update
{
    public sealed class UpdateSourceLinkCategoryCommandValidator : AbstractValidator<UpdateSourceLinkCategoryCommand>
    {
        public UpdateSourceLinkCategoryCommandValidator()
        {
            ushort maxTextLength = 4000;

            RuleFor(command => command.SourceLinkCategoryContentDto.SourceLinkId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage(SourceErrors.UpdateSourceLinkCategoryCommandValidatorSourceLinkIdeIsRequiredError);

            RuleFor(command => command.SourceLinkCategoryContentDto.StreetcodeId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage(SourceErrors.UpdateSourceLinkCategoryCommandValidatorStreetcodeIdeIsRequiredError);

            RuleFor(command => command.SourceLinkCategoryContentDto.Text)
                .MaximumLength(maxTextLength)
                .WithMessage(SourceErrors.UpdateStreetcodeCategoryContentCommandTextLengthMaxLengthError);
        }
    }
}
