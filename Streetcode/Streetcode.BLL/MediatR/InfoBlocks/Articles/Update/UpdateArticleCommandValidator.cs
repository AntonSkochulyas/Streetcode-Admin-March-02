// Necesasry usings.
using FluentValidation;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.Update
{
    /// <summary>
    /// Validator, that validates a model inside UpdateArticleCommand.
    /// </summary>
    internal class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
    {
        // Constructor
        public UpdateArticleCommandValidator()
        {
            // Max title length
            int maxTitleLength = 50;

            // Max text length
            int maxTextLength = 15000;

            RuleFor(command => command.Article.Title)
                .MaximumLength(maxTitleLength)
                .WithMessage("Title length of article must not be longer than 50 symbols.");

            RuleFor(command => command.Article.Text)
                    .MaximumLength(maxTextLength)
                    .WithMessage("Text length of article must not be longer than 15000 symbols.");
        }
    }
}
