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
        // Constructor.
        public UpdateDictionaryItemCommandValidator()
        {
            // Max name length
            ushort maxNameLength = 50;

            // Max description length
            ushort maxDescriptionLength = 500;

            RuleFor(command => command.dictionaryItem.Word)
                .MaximumLength(maxNameLength)
                .WithMessage($"Name length of dictionary item must not be longer than {maxNameLength} symbols.");

            RuleFor(command => command.dictionaryItem.Description)
                    .MaximumLength(maxDescriptionLength)
                    .WithMessage($"Description length of dictionary item must not be longer than {maxDescriptionLength} symbols.");
        }
    }
}
