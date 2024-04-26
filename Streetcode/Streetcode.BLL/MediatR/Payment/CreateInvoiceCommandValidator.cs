// Necessary usings.
using FluentValidation;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Payment
{
    /// <summary>
    /// Validator, that validates a model inside CreateInvouceCommand.
    /// </summary>
    internal class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        // Constructor
        public CreateInvoiceCommandValidator()
        {
            RuleFor(command => command.Payment.Amount)
                .NotEmpty()
                .WithMessage(PaymentErrors.CreateInvoiceCommandValidatorAmounIsRequiredError);
        }
    }
}