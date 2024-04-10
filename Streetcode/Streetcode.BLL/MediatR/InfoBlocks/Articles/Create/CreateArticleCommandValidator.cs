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
        // Max title length
        private readonly ushort _maxTitleLength;

        // Max text length
        private readonly ushort _maxTextLength;

        // Constructor
        public CreateArticleCommandValidator()
        {
            _maxTitleLength = 50;
            _maxTextLength = 15000;

            RuleFor(command => command.newArticle.Title)
                .MaximumLength(_maxTitleLength)
                .WithMessage($"Title length of article must not be longer than {_maxTitleLength} symbols.");

            RuleFor(command => command.newArticle.Text)
                .MaximumLength(_maxTextLength)
                .WithMessage($"Text length of article must not be longer than {_maxTextLength} symbols.");
        }
    }
}
