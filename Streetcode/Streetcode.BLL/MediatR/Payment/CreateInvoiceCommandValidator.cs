using FluentValidation;

namespace Streetcode.BLL.MediatR.Payment
{
    internal class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator()
        {
            RuleFor(command => command.Payment.Amount)
                .NotEmpty()
                .WithMessage(PaymentErrors.CreateInvoiceCommandValidatorAmounIsRequiredError);
        }
    }
}
