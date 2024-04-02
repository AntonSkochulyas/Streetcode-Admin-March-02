using FluentValidation;

namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.Create
{
    public sealed class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator()
        {
            int maxTitleLength = 50;
            int maxTextLength = 15000;

            RuleFor(command => command.newArticle.Title)
                .MaximumLength(maxTitleLength)
                .WithMessage("Title length of article must not be longer than 50 symbols.");

            RuleFor(command => command.newArticle.Text)
                .MaximumLength(maxTextLength)
                .WithMessage("Text length of article must not be longer than 15000 symbols.");
        }
    }
}
