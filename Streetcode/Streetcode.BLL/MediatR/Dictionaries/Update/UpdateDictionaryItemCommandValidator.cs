using FluentValidation;

namespace Streetcode.BLL.MediatR.Dictionaries.Update
{
    internal class UpdateDictionaryItemCommandValidator : AbstractValidator<UpdateDictionaryItemCommand>
    {
        public UpdateDictionaryItemCommandValidator()
        {
            int maxNameLength = 50;
            int maxDescriptionLength = 50;

            RuleFor(command => command.dictionaryItem.Name)
                .MaximumLength(maxNameLength)
                .WithMessage("Name length of dictionary item must not be longer than 50 symbols.");

            RuleFor(command => command.dictionaryItem.Description)
                    .MaximumLength(maxDescriptionLength)
                    .WithMessage("Description length of dictionary item must not be longer than 500 symbols.");
        }
    }
}
