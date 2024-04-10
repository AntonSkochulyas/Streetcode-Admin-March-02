// Necessary usings.
using FluentValidation;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Dictionaries.Create
{
    /// <summary>
    /// Validatorm that validates a model inside CreateDictionaryItemCommand.
    /// </summary>
    public sealed class CreateDictionaryItemCommandValidator : AbstractValidator<CreateDictionaryItemCommand>
    {
        private readonly ushort _maxNameLength;
        private readonly ushort _maxDescriptionLength;
        public CreateDictionaryItemCommandValidator()
        {
            _maxNameLength = 50;
            _maxDescriptionLength = 500;

            RuleFor(command => command.newDictionaryItem.Name)
                .MaximumLength(_maxNameLength)
                .WithMessage($"Name length of dictionary item must not be longer than {_maxNameLength} symbols.");

            RuleFor(command => command.newDictionaryItem.Description)
                .MaximumLength(_maxDescriptionLength)
                .WithMessage($"Description length of dictionary item must not be longer than {_maxDescriptionLength} symbols.");
        }
    }
}
