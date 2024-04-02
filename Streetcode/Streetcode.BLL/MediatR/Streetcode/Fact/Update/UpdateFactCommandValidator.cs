using System;
using FluentValidation;
using Streetcode.BLL.MediatR.Fact.Update;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.Update
{
	public class UpdateFactCommandValidator : AbstractValidator<UpdateFactCommand>
    {
        private readonly int maxTitleLength;
        private readonly int maxFactContent;

        public UpdateFactCommandValidator()
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