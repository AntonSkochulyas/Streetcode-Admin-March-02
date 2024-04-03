using System;
using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.Create
{
	public class CreateFactCommandValidator : AbstractValidator<CreateFactCommand>
    {
		private readonly int maxTitleLength;
		private readonly int maxFactContent;

		public CreateFactCommandValidator()
		{
			maxTitleLength = 100;
			maxFactContent = 600;

			RuleFor(command => command.Fact.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .MaximumLength(maxTitleLength)
                .WithMessage($"Title length should not be longer than {maxTitleLength} symbols.");

			RuleFor(command => command.Fact.FactContent)
                .NotEmpty()
                .WithMessage("Fact Content is required.")
                .MaximumLength(maxFactContent)
                .WithMessage($"Fact Content length should not be longer than {maxFactContent} symbols.");
        }
	}
}