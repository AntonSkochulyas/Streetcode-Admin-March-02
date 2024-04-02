using FluentValidation;

namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.Update
{
    internal class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
    {
        public UpdateArticleCommandValidator()
        {
            int maxTitleLength = 50;
            int maxTextLength = 15000;

            RuleFor(command => command.article.Title)
                .MaximumLength(maxTitleLength)
                .WithMessage("Title length of article must not be longer than 50 symbols.");

            RuleFor(command => command.article.Text)
                    .MaximumLength(maxTextLength)
                    .WithMessage("Text length of article must not be longer than 15000 symbols.");
        }
    }
}
