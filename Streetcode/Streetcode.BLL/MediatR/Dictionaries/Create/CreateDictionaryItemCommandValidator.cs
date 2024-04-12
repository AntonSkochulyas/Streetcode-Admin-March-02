using FluentValidation;

namespace Streetcode.BLL.MediatR.Dictionaries.Create
{
    public sealed class CreateDictionaryItemCommandValidator : AbstractValidator<CreateDictionaryItemCommand>
    {
        public CreateDictionaryItemCommandValidator()
        {
            int maxNameLength = 50;
            int maxDescriptionLength = 50;

            RuleFor(command => command.NewDictionaryItem.Name)
                .MaximumLength(maxNameLength)
                .WithMessage("Name length of dictionary item must not be longer than 50 symbols.");

            RuleFor(command => command.NewDictionaryItem.Description)
                .MaximumLength(maxDescriptionLength)
                .WithMessage("Description length of dictionary item must not be longer than 500 symbols.");
        }
    }
}
