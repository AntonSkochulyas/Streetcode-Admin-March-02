// Necessary usings.
using FluentValidation;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Dictionaries.Update
{
    /// <summary>
    /// Validator, that validates a model inside UpdateDictionaryItemCommand.
    /// </summary>
    internal class UpdateDictionaryItemCommandValidator : AbstractValidator<UpdateDictionaryItemCommand>
    {
        // Max name length
        private readonly ushort _maxNameLength;

        // Max description length
        private readonly ushort _maxDescriptionLength;

        // Constructor.
        public UpdateDictionaryItemCommandValidator()
        {
            _maxNameLength = 50;
            _maxDescriptionLength = 500;

            RuleFor(command => command.dictionaryItem.Name)
                .MaximumLength(_maxNameLength)
                .WithMessage($"Name length of dictionary item must not be longer than {_maxNameLength} symbols.");

            RuleFor(command => command.dictionaryItem.Description)
                    .MaximumLength(_maxDescriptionLength)
                    .WithMessage($"Description length of dictionary item must not be longer than {_maxDescriptionLength} symbols.");
        }
    }
}
