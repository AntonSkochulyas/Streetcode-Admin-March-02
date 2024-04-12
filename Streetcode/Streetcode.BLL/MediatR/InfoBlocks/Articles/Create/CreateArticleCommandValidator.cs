// Necessary usings.
using FluentValidation;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.Create
{
    /// <summary>
    /// Validator, that validates a model inside CreateArticleCommand.
    /// </summary>
    public sealed class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        // Constructor
        public CreateArticleCommandValidator()
        {
            // Max title length
            int maxTitleLength = 50;

            // Max text length
            int maxTextLength = 15000;

            RuleFor(command => command.NewArticle.Title)
                .MaximumLength(maxTitleLength)
                .WithMessage("Title length of article must not be longer than 50 symbols.");

            RuleFor(command => command.NewArticle.Text)
                .MaximumLength(maxTextLength)
                .WithMessage("Text length of article must not be longer than 15000 symbols.");
        }
    }
}
