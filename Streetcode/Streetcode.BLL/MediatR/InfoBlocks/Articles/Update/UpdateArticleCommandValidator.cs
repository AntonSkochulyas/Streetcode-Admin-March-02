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
        // Max title length
        private readonly ushort _maxTitleLength;

        // Max text length
        private readonly ushort _maxTextLength;

        // Constructor
        public UpdateArticleCommandValidator()
        {
            _maxTitleLength = 50;
            _maxTextLength = 15000;

            RuleFor(command => command.article.Title)
                .MaximumLength(_maxTitleLength)
                .WithMessage($"Title length of article must not be longer than {_maxTitleLength} symbols.");

            RuleFor(command => command.article.Text)
                    .MaximumLength(_maxTextLength)
                    .WithMessage($"Text length of article must not be longer than {_maxTextLength} symbols.");
        }
    }
}
