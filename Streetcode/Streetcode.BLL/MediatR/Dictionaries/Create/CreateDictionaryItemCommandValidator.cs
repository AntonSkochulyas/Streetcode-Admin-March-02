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
        public CreateDictionaryItemCommandValidator()
        {
            int maxNameLength = 50;
            int maxDescriptionLength = 500;

            RuleFor(command => command.CreateDictionaryItemDto.Name)
                .MaximumLength(maxNameLength)
                .WithMessage("Name length of dictionary item must not be longer than 50 symbols.");

            RuleFor(command => command.CreateDictionaryItemDto.Description)
                .MaximumLength(maxDescriptionLength)
                .WithMessage("Description length of dictionary item must not be longer than 500 symbols.");
        }
    }
}
